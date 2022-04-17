using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Infrastructure.Storage;
using BlueHarvest.Core.Infrastructure.Storage.Repos;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class DbUtilities : MenuActions
{
   private readonly IMongoContext _mongoContext;
   private readonly IEnumerable<IMongoRepository> _mongoRepos;
   private readonly IStarClusterRepo _starClusterRepo;

   public DbUtilities(
      IMongoContext mongoContext,
      IEnumerable<IMongoRepository> mongoRepos,
      IStarClusterRepo starClusterRepo)
   {
      _mongoContext = mongoContext;
      _mongoRepos = mongoRepos;
      _starClusterRepo = starClusterRepo;
   }

   public void DropAndInitializeDb()
   {
      ShowTitle("Drop & Initialize Db");
      WriteLine("Deleting...");
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
      WriteLine("Deleting...");
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

   public void ListStarClusters()
   {
      ShowTitle("List Clusters");
      var clusters = _starClusterRepo.All();
      int index = 0;
      foreach (var cluster in clusters)
      {
         WriteLine($"{++index} - {cluster.Name}: {cluster.Description}");
      }      
      ShowReturn();
   }
}
