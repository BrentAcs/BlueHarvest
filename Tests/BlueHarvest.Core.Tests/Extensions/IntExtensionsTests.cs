using BlueHarvest.Core.Extensions;

namespace BlueHarvest.Core.Tests.Extensions;

public class IntExtensionsTests
{
   [TestCase(-4, ExpectedResult = true)]
   [TestCase(-3, ExpectedResult = false)]
   [TestCase(-2, ExpectedResult = true)]
   [TestCase(-1, ExpectedResult = false)]
   [TestCase(0, ExpectedResult = true)]
   [TestCase(1, ExpectedResult = false)]
   [TestCase(2, ExpectedResult = true)]
   [TestCase(3, ExpectedResult = false)]
   [TestCase(4, ExpectedResult = true)]
   public bool IsEven(int val) =>
      val.IsEven();

   [TestCase(-4, ExpectedResult = false)]
   [TestCase(-3, ExpectedResult = true)]
   [TestCase(-2, ExpectedResult = false)]
   [TestCase(-1, ExpectedResult = true)]
   [TestCase(0, ExpectedResult = false)]
   [TestCase(1, ExpectedResult = true)]
   [TestCase(2, ExpectedResult = false)]
   [TestCase(3, ExpectedResult = true)]
   [TestCase(4, ExpectedResult = false)]
   public bool IsOdd(int val) =>
      val.IsOdd();

   [TestCase(-1, ExpectedResult = false)]
   [TestCase(0, ExpectedResult = false)]
   [TestCase(1, ExpectedResult = true)]
   public bool IsPositive(int val) =>
      val.IsPositive();
      
   [TestCase(-1, ExpectedResult = true)]
   [TestCase(0, ExpectedResult = false)]
   [TestCase(1, ExpectedResult = false)]
   public bool IsNegative(int val) =>
      val.IsNegative();
}
