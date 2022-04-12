using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Tests;

public class MonikerGeneratorTests
{
   [Test]
   public void Generate_WillReturn_NonEmpty()
   {
      var sut = new MonikerGenerator(new SimpleRng());

      var moniker = sut.Generate();
      
      Assert.IsNotEmpty(moniker);
   }

   [Test]
   public void Generate_WillReturn_SameLengthAsTemplate()
   {
      var sut = new MonikerGenerator(new SimpleRng());

      var moniker = sut.Generate("AAAA-AAAA");
      
      Assert.AreEqual("AAAA-AAAA".Length, moniker.Length);
   }

}
