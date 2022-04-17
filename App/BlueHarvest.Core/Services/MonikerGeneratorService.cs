using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services;

public class MonikerGeneratorService : IMonikerGeneratorService
{
   private readonly IMonikerGenerator _generator;
   private readonly IList<string> _cache = new List<string>();

   public MonikerGeneratorService(IMonikerGenerator? generator = null)
   {
      _generator = generator ?? MonikerGenerator.Default;
   }

   public static readonly MonikerGeneratorService Default = new();

   public void Reset() =>
      _cache.Clear();

   public string Generate(string template = IMonikerGenerator.DefaultTemplate)
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
