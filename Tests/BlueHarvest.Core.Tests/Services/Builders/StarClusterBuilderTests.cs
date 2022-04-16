using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Services.Builders;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Tests.Services.Builders;

public class StarClusterBuilderTests
{
   protected IRng Rng = new SimpleRng();
   
   [Test]
   public void Build_WillReturnCluster_WithProperties()
   {
      var options = StarClusterBuilderOptions.Test;
      var sut = new StarClusterBuilder(Rng);

      var cluster = sut.Build(options);
      
      cluster.Name.Should().Be(options.Name);
   }
   
   [Test]
   public void Build_WillThrow_When_TooManySystems()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      options.DesiredPlanetarySystems = new DesiredAmount(11);
      options.DesiredDeepSpaceObjects = new DesiredAmount(0);

      var sut = new StarClusterBuilder(Rng);

      sut.Invoking(s => s.Build(options))
         .Should().Throw<BuilderException>();
   }
   
   [Test]
   public void Build_WillThrow_When_TooManyDeepSpaceObjects()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      options.DesiredPlanetarySystems = new DesiredAmount(0);
      options.DesiredDeepSpaceObjects = new DesiredAmount(11);

      var sut = new StarClusterBuilder(Rng);

      sut.Invoking(s => s.Build(options))
         .Should().Throw<BuilderException>();
   }

   [Test]
   public void Build_WillThrow_When_Options_DesiredPlanetarySystems_IsNull()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      options.DesiredPlanetarySystems = null;

      var sut = new StarClusterBuilder(Rng);

      sut.Invoking(s => s.Build(options))
         .Should().Throw<ArgumentPropertyNullException>();
   }
}
