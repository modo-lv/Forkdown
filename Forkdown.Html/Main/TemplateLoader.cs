using System;
using System.IO;
using System.Threading.Tasks;
using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using Path = Fluent.IO.Path;
#pragma warning disable 1591

namespace Forkdown.Html.Main {
  public class TemplateLoader : ITemplateLoader {
    private readonly Path _dir;

    public TemplateLoader(Path dir) {
      this._dir = dir;
    }

    /// <inheritdoc />
    public String GetPath(TemplateContext context, SourceSpan callerSpan, String templateName) {
      return this._dir.Combine(templateName + ".scriban-html").FullPath;
    }

    /// <inheritdoc />
    public String Load(TemplateContext context, SourceSpan callerSpan, String templatePath) {
      return this.LoadAsync(context, callerSpan, templatePath).Result;
    }

    /// <inheritdoc />
    public async ValueTask<String> LoadAsync(TemplateContext context, SourceSpan callerSpan, String templatePath) {
      return await File.ReadAllTextAsync(templatePath);
    }
  }
}