using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Validators;

public abstract class EllipsoidValidator<T> : AbstractValidator<Ellipsoid>
{
   protected EllipsoidValidator()
   {
      RuleFor(p => p.XRadius).GreaterThan(0);
      RuleFor(p => p.YRadius).GreaterThan(0);
      RuleFor(p => p.ZRadius).GreaterThan(0);
   }
}

