using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Validators;

public static class InsideEllipsoidValidatorExtensions
{
   public static IRuleBuilderOptions<T, Ellipsoid> InsideEllipsoid<T>(
      this IRuleBuilder<T, Ellipsoid> ruleBuilder, double? maxXRadius = default,
      double? maxYRadius = default, double? maxZRadius = default)
   {
      var validator = new InsideEllipsoidValidator<T>(maxXRadius, maxYRadius, maxZRadius);
      return ruleBuilder.SetValidator(validator);
   }
}
