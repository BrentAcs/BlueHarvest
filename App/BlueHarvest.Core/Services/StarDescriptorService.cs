using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services;

public class StarDescriptorService
{
   public class RequestRandom : IRequest<StarDescriptor>
   {
   }

   public class Handler : IRequestHandler<RequestRandom, StarDescriptor>
   {
      private readonly IStarDescriptorRepo _repo;
      private readonly IRng _rng;

      public Handler(IStarDescriptorRepo repo, IRng rng)
      {
         _repo = repo;
         _rng = rng;
      }

      public Task<StarDescriptor> Handle(RequestRandom request, CancellationToken cancellationToken)
      {
         var table = GetChanceTable();
         double roll = _rng.Next(0, 100.0);
         var descriptor = table?.GetItem(roll);
         return Task.FromResult(descriptor)!;
      }

      private IChanceTable<StarDescriptor>? GetChanceTable()
      {
         var table = new ChanceTable<StarDescriptor>();

         foreach (var descriptor in _repo.All())
         {
            table.Add(descriptor, descriptor.Chance);
         }

         return table;
      }
   }
}
