using BlueHarvest.Core.Utilities;
using BlueHarvest.Core.Validators;
using NUnit.Framework.Internal.Execution;

namespace BlueHarvest.Core.Tests.Validators;

public class MinMaxValidatorTests
{
   [Test]
   [TestCaseSource(nameof(ValidInsideInclusive))]
   public void InsideInclusiveValidator_WillBeTrue<T>(MinMax<T> minmax, T min, T max)
      where T : IComparable<T?>, IComparable =>
      Assert.IsTrue(new InsideInclusiveValidator<T>(min, max).Validate(minmax).IsValid);

   private static IEnumerable<TestCaseData> ValidInsideInclusive()
   {
      yield return new TestCaseData(new MinMax<int>(5, 10), 5, 10);
      yield return new TestCaseData(new MinMax<double>(5.0, 10.0), 5.0, 10.0);
      yield return new TestCaseData(new MinMax<double>(5.0, 10.0), 5.0, 10.0);
   }

   [Test]
   [TestCaseSource(nameof(InValidInsideInclusive))]
   public void InsideInclusiveValidator_WillBeFalse<T>(MinMax<T> minmax, T min, T max)
      where T : IComparable<T?>, IComparable =>
      Assert.IsFalse(new InsideInclusiveValidator<T>(min, max).Validate(minmax).IsValid);

   private static IEnumerable<TestCaseData> InValidInsideInclusive()
   {
      yield return new TestCaseData(new MinMax<int>(4, 11), 5, 10);
      yield return new TestCaseData(new MinMax<double>(4, 11), 1.0, 5.0);
      yield return new TestCaseData(new MinMax<double>(4.99999, 10.11111), 1.0, 5.0);
      yield return new TestCaseData(new MinMax<double>(2, 12), 3, 10);
   }

   [Test]
   [TestCaseSource(nameof(ValidInsideExclusive))]
   public void InsideExclusiveValidator_WillBeTrue<T>(MinMax<T> minmax, T min, T max)
      where T : IComparable<T?>, IComparable =>
      Assert.IsTrue(new InsideExclusiveValidator<T>(min, max).Validate(minmax).IsValid);

   private static IEnumerable<TestCaseData> ValidInsideExclusive()
   {
      yield return new TestCaseData(new MinMax<int>(6, 9), 5, 10);
      yield return new TestCaseData(new MinMax<double>(5.5, 9.5), 5, 10);
      yield return new TestCaseData(new MinMax<double>(6.0, 9.0), 5, 10);
   }

   [Test]
   [TestCaseSource(nameof(InValidInsideExclusive))]
   public void InsideExclusiveValidator_WillBeFalse<T>(MinMax<T> minmax, T min, T max)
      where T : IComparable<T?>, IComparable =>
      Assert.IsFalse(new InsideExclusiveValidator<T>(min, max).Validate(minmax).IsValid);

   private static IEnumerable<TestCaseData> InValidInsideExclusive()
   {
      yield return new TestCaseData(new MinMax<int>(5, 10), 5, 10);
      yield return new TestCaseData(new MinMax<double>(5, 10), 5, 10);
   }
}
