using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.Core.IntegrationTests;

public class StarClusterRepoTests : BaseMongoIntegrationTests
{
   private IStarClusterRepo? _repo = null;

   [OneTimeSetUp]
   public override async Task OneTimeSetUp()
   {
      await base.OneTimeSetUp();
      _repo = new StarStarClusterRepo(MongoContext, new Mock<ILogger<StarStarClusterRepo>>().Object);
      await _repo.InitializeIndexesAsync();
      await _repo.SeedDataAsync();
   }

   [OneTimeTearDown]
   public override void OneTimeTearDown() => base.OneTimeTearDown();

   [Test, Order(100)]
   public async Task InsertOneAsync_WillThrow_MongoWriteException_WhenClusterNameExists_SameCase()
   {
      await (_repo?.InsertOneAsync(new StarCluster {Name = "Brent's"})!).ConfigureAwait(false);
      Assert.ThrowsAsync<MongoWriteException>(async () =>
      {
         await (_repo?.InsertOneAsync(new StarCluster {Name = "Brent's"})!).ConfigureAwait(false);
      });
   }

   [Test, Order(110)]
   public async Task InsertOneAsync_WillThrow_MongoWriteException_WhenClusterNameExists_MixedCase()
   {
      await (_repo?.InsertOneAsync(new StarCluster {Name = "Test"})!).ConfigureAwait(false);
      Assert.ThrowsAsync<MongoWriteException>(async () =>
      {
         await (_repo?.InsertOneAsync(new StarCluster {Name = "test"})!).ConfigureAwait(false);
      });
   }
}
