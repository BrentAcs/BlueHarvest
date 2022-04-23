using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Infrastructure.Storage;
using BlueHarvest.Core.Models;
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

   private void Drop()
   {
      if (!AnsiConsole.Confirm("Are you [yellow]sure[/] you wish to drop Db?", false))
         return;

      WriteLine("Dropping...");
      _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);
      RuntimeAppState.Instance.CurrentCluster = null;
   }

   private void Initialize()
   {
      WriteLine("Initializing...");
      Task.WaitAll(_mongoRepos.InitializeAllIndexesAsync());
      WriteLine("Seeding...");
      Task.WaitAll(_mongoRepos.SeedAllDataAsync());
   }

   public void DropAndInitializeDb()
   {
      ShowTitle("Drop & Initialize Db");
      Drop();
      Initialize();
      ShowReturn();
   }

   public void DropDb()
   {
      ShowTitle("Drop Db");
      Drop();
      ShowReturn();
   }

   public void InitializeDb()
   {
      ShowTitle("Initialize Db");
      Initialize();
      ShowReturn();
   }
}
