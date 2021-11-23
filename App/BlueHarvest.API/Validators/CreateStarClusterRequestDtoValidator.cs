using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.API.Validators;

public class CreateStarClusterRequestDtoValidator : AbstractValidator<CreateStarClusterRequestDto>
{
   public CreateStarClusterRequestDtoValidator(IStarClusterRepo repo)
   {
      RuleFor(dto => dto.Name).CustomAsync(async (name, context, cancellationToken) =>
      {
         var result = await repo.FindByNameAsync(name, cancellationToken).ConfigureAwait(false);
         if (result.Any(cancellationToken: cancellationToken))
         {
            context.AddFailure($"A cluster with the name '{name}' already exists.");
         }
      });

      // RuleFor(p => p.Name)
      //    .NotNull()
      //    .NotEmpty();
   }
}
