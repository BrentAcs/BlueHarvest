using BlueHarvest.Core.Misc;

namespace BlueHarvest.Core.Validators;

// InsideExclusive
// InsideInclusive
// OutsideInclusive
// OutsideExclusive

public abstract class MinMaxValidator : AbstractValidator<MinMax<int>>
{
}

public class InsideExclusiveValidator : MinMaxValidator
{
   public InsideExclusiveValidator(int min, int max)
   {
      RuleFor(p => p.Min).GreaterThan(min);
      RuleFor(p => p.Max).LessThan(max);
   }
}

public static class MinMaxValidatorExtensions
{
   public static IRuleBuilderOptions<T, MinMax<int>> InsideInclusive<T>(
      this IRuleBuilder<T, MinMax<int>> ruleBuilder, int min, int max)
   {
      var validator = new InsideExclusiveValidator(min, max);
      return ruleBuilder.SetValidator(validator);
   }
}


