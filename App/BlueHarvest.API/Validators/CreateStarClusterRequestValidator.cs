using BlueHarvest.API.DTOs.Cosmic;
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
      RuleFor(p => p.ClusterSize).NotNull().SetValidator( new InsideEllipsoidValidator(100,100,50));

      RuleFor(dto => dto.Name).CustomAsync(async (name, context, cancellationToken) =>
      {
         var result = await repo.FindByNameAsync(name, cancellationToken).ConfigureAwait(false);
         if (result.Any(cancellationToken: cancellationToken))
         {
            context.AddFailure($"A cluster with the name '{name}' already exists.");
         }
      });


// public Ellipsoid? ClusterSize { get; set; }
// public MinMax<double>? DistanceBetweenSystems { get; set; }
//
// public PlanetarySystemBuilderOptions? PlanetarySystemOptions { get; set; }

      // TODO: Finish
   }
}
