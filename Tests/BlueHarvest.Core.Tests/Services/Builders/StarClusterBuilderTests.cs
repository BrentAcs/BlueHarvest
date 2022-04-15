﻿using BlueHarvest.Core.Services.Builders;
using BlueHarvest.Core.Utilities;
using FluentAssertions;
using MongoDB.Driver;

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
      
      cluster.Name.Should().Be(options.Name);
   }
   
   [Test]
   public void Build_WillThrow_WhenTooManySystems()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      options.DesiredPlanetarySystems = new DesiredAmount(11);
      options.DesiredDeepSpaceObjects = new DesiredAmount(0);

      var sut = new StarClusterBuilder(rngMock.Object);

      sut.Invoking(s => s.Build(options))
         .Should().Throw<BuilderException>();
   }
   
   [Test]
   public void Build_WillThrow_WhenTooManyDeepSpaceObjects()
   {
      Mock<IRng> rngMock = new Mock<IRng>();
      var options = StarClusterBuilderOptions.Test;
      options.DesiredPlanetarySystems = new DesiredAmount(0);
      options.DesiredDeepSpaceObjects = new DesiredAmount(11);

      var sut = new StarClusterBuilder(rngMock.Object);

      sut.Invoking(s => s.Build(options))
         .Should().Throw<BuilderException>();
   }
}
