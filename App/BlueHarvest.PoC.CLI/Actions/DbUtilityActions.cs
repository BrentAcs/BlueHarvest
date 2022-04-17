using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Infrastructure.Storage;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class DbUtilityActions : MenuActions
{
   private readonly IMongoContext _mongoContext;
   private readonly IEnumerable<IMongoRepository> _mongoRepos;

   public DbUtilityActions(
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
