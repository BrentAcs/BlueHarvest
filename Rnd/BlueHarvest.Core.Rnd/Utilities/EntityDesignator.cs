using System.Diagnostics.Tracing;

namespace BlueHarvest.Core.Rnd.Utilities;

public interface IEntityMonikerGeneratorService
{
   // TODO: create options and implement. For now, assume ...
   // bool AllowDuplicates { get; set; } = default false;
   // bool CaseInsensitive { get; set; } = default false;

   void Reset();
   string Generate(string template = IEntityMonikerGenerator.DefaultTemplate);
}

public class EntityMonikerGeneratorService : IEntityMonikerGeneratorService
{
   private readonly IEntityMonikerGenerator _generator;
   private readonly IList<string> _cache = new List<string>();

   public EntityMonikerGeneratorService(IEntityMonikerGenerator? generator = null)
   {
      _generator = generator ?? EntityMonikerGenerator.Default;
   }

   public static readonly EntityMonikerGeneratorService Default = new();

   public void Reset() =>
      _cache.Clear();

   public string Generate(string template = IEntityMonikerGenerator.DefaultTemplate)
   {
      do
      {
         var moniker = _generator.Generate(template);
         if (!_cache.Contains(moniker, StringComparer.OrdinalIgnoreCase))
         {
            _cache.Add(moniker);
            return moniker;
         }
      } while (true);
   }
}

public interface IEntityMonikerGenerator
{
   const string DefaultTemplate = @"AAA-AAA";

   string Generate(string template = DefaultTemplate);
}

public class EntityMonikerGenerator : IEntityMonikerGenerator
{
   private readonly IRng _rng;

   private const string Letters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   private const string Numbers = @"0123456789";
   private const string AlphaNumeric = Letters + Numbers;

   public static readonly IEntityMonikerGenerator Default = new EntityMonikerGenerator(new SimpleRng());

   public EntityMonikerGenerator(IRng rng)
   {
      _rng = rng;
   }

   public string Generate(string template = IEntityMonikerGenerator.DefaultTemplate) =>
      template.Aggregate(string.Empty, (current, t) => current + t switch
      {
         'L' => Letters[ _rng.Next(0, Letters.Length) ],
         'N' => Numbers[ _rng.Next(0, Numbers.Length) ],
         'A' => AlphaNumeric[ _rng.Next(0, AlphaNumeric.Length) ],
         _ => t
      });
}
