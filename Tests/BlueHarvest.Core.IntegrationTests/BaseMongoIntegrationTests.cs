using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.IntegrationTests;

public abstract class BaseMongoIntegrationTests
{
   private IMongoContext? _mongoContext;

   protected IMongoContext? MongoContext => _mongoContext;

   public virtual Task OneTimeSetUp()
   {
      var config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();

      var section = config.GetSection("MongoDb");
      var settings = section.Get<MongoDbSettings>();
      settings.DatabaseName = $"{settings.DatabaseName}-{Guid.NewGuid()}";
      _mongoContext = new MongoContext(settings);
      TestContext.Progress.WriteLine($"Mongo Context created: '{_mongoContext?.Settings?.DatabaseName}'");
      return Task.CompletedTask;
   }

   public virtual void OneTimeTearDown()
   {
      TestContext.Progress.WriteLine($"Dropping db: '{_mongoContext?.Settings?.DatabaseName}'");
      _mongoContext?.Db?.Client?.DropDatabase(_mongoContext?.Settings?.DatabaseName);
   }
}
