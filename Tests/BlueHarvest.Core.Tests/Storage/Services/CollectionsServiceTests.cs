using BlueHarvest.Core.Storage.Services;

namespace BlueHarvest.Core.Tests.Storage.Services;

public class CollectionsServiceTests
{
   [Test]
   public void CollectionNames_WillReturn_Two()
   {
      var svc = new CollectionsService();

      Assert.AreEqual(2, svc.CollectionNames.Count());
   }
}
