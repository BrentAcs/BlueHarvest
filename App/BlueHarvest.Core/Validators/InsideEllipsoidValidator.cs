namespace BlueHarvest.Core.Validators;

public class InsideEllipsoidValidator : EllipsoidValidator
{
   public InsideEllipsoidValidator(double? maxXRadius=default, double? maxYRadius=default, double? maxZRadius=default)
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
