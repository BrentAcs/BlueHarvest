using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Validators;

public class EllipsoidValidator : AbstractValidator<Ellipsoid>
{
   public EllipsoidValidator(double? maxXRadius, double? maxYRadius, double? maxZRadius)
   {
      RuleFor(p => p.XRadius)
         .LessThanOrEqualTo( p=> maxXRadius)
         .When(p => maxXRadius.HasValue);
      RuleFor(p => p.YRadius)
         .LessThanOrEqualTo( p=> maxYRadius)
         .When(p => maxYRadius.HasValue);
      RuleFor(p => p.ZRadius)
         .LessThanOrEqualTo( p=> maxZRadius)
         .When(p => maxZRadius.HasValue);
   }
}
