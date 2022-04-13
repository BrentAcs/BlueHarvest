using BlueHarvest.Core.Services.Builders;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Tests.Services.Builders;

public class StarClusterBuilderTests
{
   [Test]
   public void Build_WillReturnCluster_WithProperties()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      var sut = new StarClusterBuilder(rngMock.Object);

      var cluster = sut.Build(options);
      
      Assert.AreEqual(options.Name, cluster.Name);
   }
   
   [Test]
   public void Build_WillThrow_WhenTooManySystems()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      options.SystemAmount = new SystemAmount(11);
      var sut = new StarClusterBuilder(rngMock.Object);

      Assert.Throws<BuilderException>(() => sut.Build(options));
   }
}
