using BlueHarvest.Core.Utilities;
using FluentAssertions;

namespace BlueHarvest.Core.Tests;

public class MonikerGeneratorTests
{
   [Test]
   public void Generate_WillReturn_NonEmpty()
   {
      var sut = new MonikerGenerator(new SimpleRng());

      var moniker = sut.Generate();

      moniker.Should().NotBeEmpty();
   }

   [Test]
   public void Generate_WillReturn_SameLengthAsTemplate()
   {
      var sut = new MonikerGenerator(new SimpleRng());

      var moniker = sut.Generate("AAAA-AAAA");

      moniker.Should().HaveLength(9).And.Contain("-");
   }
}
