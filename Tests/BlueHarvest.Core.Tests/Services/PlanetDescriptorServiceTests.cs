using BlueHarvest.Core.Models;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Utilities;
using Moq;

namespace BlueHarvest.Core.Tests.Services;

public class PlanetDescriptorServiceTests
{
   [TestCase(0.1, ExpectedResult=PlanetaryZone.Inner)]
   [TestCase(0.2498, ExpectedResult=PlanetaryZone.Inner)]
   [TestCase(0.2499, ExpectedResult=PlanetaryZone.Inner)]
   [TestCase(0.25, ExpectedResult=PlanetaryZone.InnerHabitable)]
   [TestCase(3.499, ExpectedResult=PlanetaryZone.Habitable)]
   [TestCase(3.5, ExpectedResult=PlanetaryZone.OuterHabitable)]
   [TestCase(4.999, ExpectedResult=PlanetaryZone.OuterHabitable)]
   [TestCase(5.0, ExpectedResult=PlanetaryZone.Outer)]
   public PlanetaryZone IdentifyPlanetaryZone_WillReturnCorrectZone(double distance)
   {
      var rngMock = new Mock<IRng>();
      
      return new PlanetDescriptorService(rngMock.Object).IdentifyPlanetaryZone(distance);
   }
}
