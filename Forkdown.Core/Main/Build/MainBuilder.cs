using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using static MoreLinq.Extensions.GroupAdjacentExtension;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public readonly BuilderStorage Storage = new BuilderStorage();
    public readonly IList<Type> Workers = new List<Type>();
    public BuildConfig? Config = null;
    private readonly ICollection<String> _finishedDocs = new Collection<String>();

    public Document Build(String markdown, ProjectPath? file = null) =>
      this.Build(FromMarkdown.ToForkdown(markdown, file));

    public Document Build(Document doc) =>
      this.Build(new[] { doc }).Single();

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public IEnumerable<Document> Build(IEnumerable<Document> documents) {
      documents = documents.ToList();
      var duplicates = this._finishedDocs.Intersect(
        documents.Select(_ => _.ProjectFileId).Where(_ => _.NotBlank())
      ).ToList();
      if (duplicates.Any())
        throw new Exception($"Documents {duplicates.StringJoin(", ")} already processed with this builder.");


      // Group by processing type
      var workerGroups = this.Workers.GroupAdjacent(_ =>
        _.GetInterfaces().SingleOrDefault(i => i.Name.IsOneOf(nameof(IProjectWorker), nameof(IDocumentWorker)))
      );

      workerGroups.ForEach(wg => {
        if (wg.Key == typeof(IProjectWorker)) {
          documents = wg.Aggregate(documents, (docs, wType) =>
            docs.Select(doc =>
              (Document)ElementBuilder.Build(doc, new[] { this.CreateWorker(wType) }, new BuilderStorage())
            ).ToList()
          );
        }
        else if (wg.Key == typeof(IDocumentWorker)) {
          documents = documents.Select(_ => wg.Aggregate(_, (doc, wType) =>
            (Document)ElementBuilder.Build(doc, new[] { this.CreateWorker(wType) }, new BuilderStorage()))
          ).ToList();
        }
        else {
          documents = documents.Select(doc => {
            var workers = wg.Select(this.CreateWorker).ToList();
            return (Document)ElementBuilder.Build(doc, workers, new BuilderStorage());
          }).ToList();
        }
      });

      return documents.Select(doc => {
        if (doc.ProjectFileId.NotBlank())
          this._finishedDocs.Add(doc.ProjectFileId);
        return doc;
      });
    }

    protected Worker CreateWorker(Type type) {
      var worker = (Worker) Activator.CreateInstance(type)!;
      worker.Builder = this;
      return worker;
    }

    public MainBuilder AddWorker<T>() where T : Worker {
      if (this.Workers.Contains(typeof(T)))
        throw new Exception($"{nameof(T)} is already registered.");
      this.Workers.Add(typeof(T));
      return this;
    }
  }
}
