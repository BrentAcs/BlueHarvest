using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Validators;

namespace BlueHarvest.API.Validators;

public class PlanetarySystemBuilderOptionsValidator : AbstractValidator<PlanetarySystemBuilderOptions>
{
   private const double SystemRadiusMin = 10.0;
   private const double SystemRadiusMax = 150.0;
   private const double DistanceMultiplierMinLength = 1.0;
   private const double DistanceMultiplierMaxLength = 2.0;

   public PlanetarySystemBuilderOptionsValidator()
   {
      RuleFor(options => options.SystemRadius).NotNull()
         .InsideInclusive(SystemRadiusMin, SystemRadiusMax);
      RuleFor(options => options.DistanceMultiplier)
         .InclusiveBetween(DistanceMultiplierMinLength, DistanceMultiplierMaxLength);
   }
}
