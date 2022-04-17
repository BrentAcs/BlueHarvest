using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services;

public interface IMonikerGeneratorService
{
   // TODO: create options and implement. For now, assume ...
   // bool AllowDuplicates { get; set; } = default false;
   // bool CaseInsensitive { get; set; } = default false;

   void Reset();
   string Generate(string template = IMonikerGenerator.DefaultTemplate);
}
