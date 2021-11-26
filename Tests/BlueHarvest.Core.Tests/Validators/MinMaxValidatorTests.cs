using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Validators;

namespace BlueHarvest.Core.Tests.Validators;

public class MinMaxValidatorTests
{
   private static IEnumerable<TestCaseData> ValidInsideExclusive()
   {
      yield return new TestCaseData(1, 5, new MinMax<int>(2,4));
   }

   [Test]
   [TestCaseSource(nameof(ValidInsideExclusive))]
   public void InsideExclusiveValidator_WillBeTrue(int min, int max, MinMax<int> minmax) =>
      Assert.IsTrue( new InsideExclusiveValidator(min, max).Validate(minmax).IsValid );
}
