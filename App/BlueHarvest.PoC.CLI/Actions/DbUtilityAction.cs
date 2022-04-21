using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Infrastructure.Storage;
using Spectre.Console.Rendering;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class DbUtilityAction : MenuAction
{
   private readonly IMongoContext _mongoContext;
   private readonly IEnumerable<IMongoRepository> _mongoRepos;

   public DbUtilityAction(
      IMongoContext mongoContext,
      IEnumerable<IMongoRepository> mongoRepos)
   {
      _mongoContext = mongoContext;
      _mongoRepos = mongoRepos;
   }

   public void DropAndInitializeDb()
   {
      ShowTitle("Drop & Initialize Db");
      if (!AnsiConsole.Confirm("Are you [yellow]sure[/] you wish to drop Db?", false))
         return;
      
      WriteLine("Dropping...");
      _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);
      WriteLine("Initializing...");
      Task.WaitAll(_mongoRepos.InitializeAllIndexesAsync());
      WriteLine("Seeding...");
      Task.WaitAll(_mongoRepos.SeedAllDataAsync());
      ShowReturn();
   }
   
   public void DropDb()
   {
      ShowTitle("Drop Db");
      if (!AnsiConsole.Confirm("Are you [yellow]sure[/] you wish to drop Db?", false))
         return;
      
      WriteLine("Dropping...");
      _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);
      ShowReturn();
   }

   public void InitializeDb()
   {
      ShowTitle("Initialize Db");
      WriteLine("Initializing...");
      Task.WaitAll(_mongoRepos.InitializeAllIndexesAsync());
      WriteLine("Seeding...");
      Task.WaitAll(_mongoRepos.SeedAllDataAsync());
      ShowReturn();
   }
}

public static class ColorExtensions
{
   public static Color GetInvertedColor(this Color color)
   {
      return GetLuminance(color) < 140 ? Color.White : Color.Black;
   }

   private static float GetLuminance(this Color color)
   {
      return (float)((0.2126 * color.R) + (0.7152 * color.G) + (0.0722 * color.B));
   }
}

