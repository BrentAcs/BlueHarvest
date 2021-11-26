using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Validators;

namespace BlueHarvest.API.Validators;

public class CreateStarClusterRequestValidator : AbstractValidator<CreateStarClusterRequest>
{
   public CreateStarClusterRequestValidator(IStarClusterRepo repo)
   {
      RuleFor(p => p.Name).NotEmpty().MinimumLength(4).MaximumLength(40);
      RuleFor(p => p.Description).NotEmpty().MinimumLength(4).MaximumLength(256);
      RuleFor(p => p.Owner).NotEmpty().MinimumLength(4).MaximumLength(40);
      RuleFor(p => p.ClusterSize).NotNull().InsideEllipsoid(100, 100, 50);
      RuleFor(p => p.DistanceBetweenSystems).NotNull().InsideInclusive(3, 10);
      RuleFor(dto => dto.Name).CustomAsync(async (name, context, cancellationToken) =>
      {
         var result = await repo.FindByNameAsync(name, cancellationToken).ConfigureAwait(false);
         if (result.Any(cancellationToken: cancellationToken))
         {
            context.AddFailure($"A cluster with the name '{name}' already exists.");
         }
      });

      // TODO: Finish
      // public PlanetarySystemBuilderOptions? PlanetarySystemOptions { get; set; }
   }
}

// public static IRuleBuilderOptions<T, string> EmailAddress<T>(
//    this IRuleBuilder<T, string> ruleBuilder,
//    EmailValidationMode mode = EmailValidationMode.AspNetCoreCompatible)
// {
//    PropertyValidator<T, string> validator = mode == EmailValidationMode.AspNetCoreCompatible ? (PropertyValidator<T, string>) new AspNetCoreCompatibleEmailValidator<T>() : (PropertyValidator<T, string>) new EmailValidator<T>();
//    return ruleBuilder.SetValidator((IPropertyValidator<T, string>) validator);
// }
