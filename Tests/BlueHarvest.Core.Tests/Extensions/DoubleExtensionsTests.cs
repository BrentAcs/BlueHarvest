using BlueHarvest.Core.Extensions;

namespace BlueHarvest.Core.Tests.Extensions
{
   public class DoubleExtensionsTests
   {
      [TestCase(1.0, 1.0, ExpectedResult = true)]
      [TestCase(1.1, 1.1, ExpectedResult = true)]
      [TestCase(1.01, 1.01, ExpectedResult = true)]
      [TestCase(1.001, 1.001, ExpectedResult = true)]
      [TestCase(1.0001, 1.0001, ExpectedResult = true)]
      [TestCase(1.0001, 1.00001, ExpectedResult = true)]
      [TestCase(1.0, 1.1, ExpectedResult = false)]
      [TestCase(1.1, 1.01, ExpectedResult = false)]
      [TestCase(1.01, 1.001, ExpectedResult = false)]
      [TestCase(1.001, 1.0001, ExpectedResult = false)]
      public bool EqualsToPrecisionDefault(double lhs, double rhs) 
         => lhs.EqualsToPrecision(rhs);
   }
}
