using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Build.Builders;
using Forkdown.Core.Elements;
using Microsoft.Extensions.Logging;
using Simpler.NetCore;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build {
  public partial class ForkdownBuild {
    private readonly ILogger<ForkdownBuild> _logger;

    public readonly IDictionary<Type, Builder> Builders = Nil.D<Type, Builder>();

    public readonly IDictionary<Type, Maybe<Object>> Results = Nil.D<Type, Maybe<Object>>();

    public ForkdownBuild(ILogger<ForkdownBuild>? logger = null) {
      _logger = logger ?? Program.Logger<ForkdownBuild>();
    }

    public ForkdownBuild AddBuilder(Type tBuilder, Boolean includeDependencies = true) {
      if (!this.Builders.ContainsKey(tBuilder)) {
        var builder = (Builder) Activator.CreateInstance(tBuilder)!;
        this.Builders[tBuilder] = builder;
        if (includeDependencies)
          builder.MustHaveRun.ForEach(_ => this.AddBuilder(_, includeDependencies));
      }
      return this;
    }

    public ForkdownBuild AddBuilder<TBuilder>(Boolean includeDependencies = true) where TBuilder : Builder =>
      this.AddBuilder(typeof(TBuilder), includeDependencies);

    public Document Build(String markdown, ProjectPath? file = null) =>
      this.Build(FromMarkdown.ToForkdown(markdown, file));

    protected IEnumerable<Builder> QueueBuilders(
      IEnumerable<Builder> builders,
      IDictionary<Type, ICollection<Type>>? deps = null
    ) {
      deps ??= Nil.D<Type, ICollection<Type>>();

      var result = Nil.L<Builder>();

      builders.ForEach(_ => {
        this.QueueBuilders(_.MustHaveRun.Select(type => this.Builders[type]), deps).ForEach(db => {
          var bt = _.GetType();
          var dt = db.GetType();
          if (deps.GetOrAdd(dt, Nil.L<Type>()).Contains(bt))
            throw new Exception($"Circular reference between {bt} and {dt}");
          if (!result.Contains(db))
            result.Add(db);
          deps.GetOrAdd(bt, Nil.L<Type>()).Add(dt);
        });
        if (!result.Contains(_))
          result.Add(_);
      });

      return result;
    }

    public TElement Build<TElement>(TElement input) where TElement : Element {
      var ran = Nil.S<Type>();

      var queue = this.QueueBuilders(this.Builders.Values);

      return queue.Aggregate(input, (element, builder) => {
        if (builder.MustHaveRun.Any() && !builder.MustHaveRun.Overlaps(ran)) {
          var missing = builder.MustHaveRun.Except(ran).Select(_ => _.Name);
          throw new Exception($"Cannot run {builder.GetType().Name}: must run {String.Join(", ", missing)} first!");
        }
        try {
          return builder.BuildTree(element, this.Results.GetOrAdd(builder.GetType(), May.Be<Object>(null!)));
        }
        finally {
          ran.Add(builder.GetType());
        }
      });
    }
  }
}
