using BlueHarvest.Core.Storage.Services;

namespace BlueHarvest.Core.Tests.Storage.Services;

[TestFixture]
public class CollectionsServiceTests
{
   [Test]
   public void CollectionNames_WillReturn_Two()
   {
      var svc = new CollectionsService();

      Assert.AreEqual(3, svc.CollectionNames.Count());
   }
}
