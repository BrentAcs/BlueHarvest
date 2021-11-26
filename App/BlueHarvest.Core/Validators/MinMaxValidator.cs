using BlueHarvest.Core.Misc;

namespace BlueHarvest.Core.Validators;

// TODO: OutsideInclusive
// TODO: OutsideExclusive

public abstract class MinMaxValidator<T> : AbstractValidator<MinMax<T>>
{
}

public class InsideInclusiveValidator<T> : MinMaxValidator<T> where T : IComparable<T?>, IComparable
{
   public InsideInclusiveValidator(T min, T max)
   {
      RuleFor(p => p.Min).GreaterThanOrEqualTo(min)
         .WithMessage($"'{min}' must be greater than or equal to Min value");
      RuleFor(p => p.Max).LessThanOrEqualTo(max)
         .WithMessage($"'{max}' must be less than or equal to Max value");
   }
}

public class InsideExclusiveValidator<T> : MinMaxValidator<T> where T : IComparable<T?>, IComparable
{
   public InsideExclusiveValidator(T min, T max)
   {
      RuleFor(p => p.Min).GreaterThan(min)
         .WithMessage($"'{min}' must be greater than to Min value");
      RuleFor(p => p.Max).LessThan(max)
         .WithMessage($"'{max}' must be less than to Max value");
   }
}

public static class MinMaxValidatorExtensions
{
   public static IRuleBuilderOptions<T, MinMax<TV>> InsideInclusive<T, TV>(
      this IRuleBuilder<T, MinMax<TV>> ruleBuilder, TV min, TV max) where TV : IComparable<TV?>, IComparable
   {
      var validator = new InsideInclusiveValidator<TV>(min, max);
      return ruleBuilder.SetValidator(validator);
   }

   public static IRuleBuilderOptions<T, MinMax<TV>> InsideExclusive<T, TV>(
      this IRuleBuilder<T, MinMax<TV>> ruleBuilder, TV min, TV max) where TV : IComparable<TV?>, IComparable
   {
      var validator = new InsideExclusiveValidator<TV>(min, max);
      return ruleBuilder.SetValidator(validator);
   }
}


