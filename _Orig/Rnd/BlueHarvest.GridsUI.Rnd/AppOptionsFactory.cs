using System.Reflection;
using BlueHarvest.Core.Extensions;

namespace BlueHarvest.GridsUI.Rnd;

public static class AppOptionsFactory
{
   private const string Filename = "app-options.json";

   public static IAppOptions? Create()
   {
      var filename =
         Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) ?? string.Empty,
            Filename);

      var options = File.Exists(filename) ? filename.FromJsonFile<AppOptions>() : new AppOptions();

      options.Filename = filename;

      return options;
   }
}
