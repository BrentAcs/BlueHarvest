using BlueHarvest.Core.Commands.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.Core.Validators;

public class CreateStarClusterValidator : AbstractValidator<CreateStarCluster>
{
   private const int NameMinLength = 4;
   private const int NameMaxLength = 40;
   private const int DescriptionMinLength = 4;
   private const int DescriptionMaxLength = 40;
   private const int OwnerMinLength = 4;
   private const int OwnerMaxLength = 40;
   private const double ClusterSizeXRadiusMax = 100.0;
   private const double ClusterSizeYRadiusMax = 100.0;
   private const double ClusterSizeZRadiusMax = 50.0;
   private const double DistanceBetweenSystemsMin = 3.0;
   private const double DistanceBetweenSystemsMax = 10.0;

   public CreateStarClusterValidator(IStarClusterRepo repo)
   {
      RuleFor(request => request.Name).NotEmpty().MinimumLength(NameMinLength).MaximumLength(NameMaxLength);
      RuleFor(request => request.Description).NotEmpty().MinimumLength(DescriptionMinLength).MaximumLength(DescriptionMaxLength);
      RuleFor(request => request.Owner).NotEmpty().MinimumLength(OwnerMinLength).MaximumLength(OwnerMaxLength);
      RuleFor(request => request.ClusterSize).NotNull()
         .InsideEllipsoid(ClusterSizeXRadiusMax, ClusterSizeYRadiusMax, ClusterSizeZRadiusMax);
      RuleFor(request => request.DistanceBetweenSystems).NotNull()
         .InsideInclusive(DistanceBetweenSystemsMin, DistanceBetweenSystemsMax);
      RuleFor(dto => dto.Name).CustomAsync(async (name, context, cancellationToken) =>
      {
         var result = await repo.FindByNameAsync(name, cancellationToken).ConfigureAwait(false);
         if (result.Any(cancellationToken: cancellationToken))
         {
            context.AddFailure($"A cluster with the name '{name}' already exists.");
         }
      });
      RuleFor(request => request.PlanetarySystemOptions).SetValidator(new PlanetarySystemBuilderOptionsValidator());
   }
}
