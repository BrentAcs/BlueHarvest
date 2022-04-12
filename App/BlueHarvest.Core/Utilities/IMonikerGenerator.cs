namespace BlueHarvest.Core.Utilities;

public interface IMonikerGenerator
{
   const string DefaultTemplate = @"AAA-AAA";

   string Generate(string template = DefaultTemplate);
}
