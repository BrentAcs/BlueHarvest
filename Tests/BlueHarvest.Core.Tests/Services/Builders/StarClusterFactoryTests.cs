using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Tests.Services.Builders;

public class StarClusterFactoryTests
{
   private static readonly IRng _rng = new SimpleRng();

   private StarClusterFactory CreateStarClusterFactory(IMock<IPlanetarySystemFactory>? planetarySystemFactoryMock=null)
   {
      planetarySystemFactoryMock ??= new Mock<IPlanetarySystemFactory>();

      return new StarClusterFactory(_rng, planetarySystemFactoryMock.Object);
   }   
   
   [Test]
   public void Build_WillReturnCluster_WithProperties()
   {
      var options = StarClusterFactoryOptions.Test;
      var sut = CreateStarClusterFactory();

      var cluster = sut.Build(options);
      
      cluster.Name.Should().Be(options.Name);
   }
   
   [Test]
   public void Build_WillThrow_When_TooManySystems()
   {
      var options = StarClusterFactoryOptions.Test;
      options.DesiredPlanetarySystems = new DesiredAmount(11);
      options.DesiredDeepSpaceObjects = new DesiredAmount(0);

      var sut = CreateStarClusterFactory();

      sut.Invoking(s => s.Build(options))
         .Should().Throw<BuilderException>();
   }
   
   [Test]
   public void Build_WillThrow_When_TooManyDeepSpaceObjects()
   {
      var options = StarClusterFactoryOptions.Test;
      options.DesiredPlanetarySystems = new DesiredAmount(0);
      options.DesiredDeepSpaceObjects = new DesiredAmount(11);

      var sut = CreateStarClusterFactory();

      sut.Invoking(s => s.Build(options))
         .Should().Throw<BuilderException>();
   }

   [Test]
   public void Build_WillThrow_When_Options_DesiredPlanetarySystems_IsNull()
   {
      var options = StarClusterFactoryOptions.Test;
      options.DesiredPlanetarySystems = null;

      var sut = CreateStarClusterFactory();

      sut.Invoking(s => s.Build(options))
         .Should().Throw<ArgumentPropertyNullException>();
   }
}
