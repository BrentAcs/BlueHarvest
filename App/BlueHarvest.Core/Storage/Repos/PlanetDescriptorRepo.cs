using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Storage.Repos;

public interface IPlanetDescriptorRepo : IMongoRepository<PlanetDescriptor>
{
}

public class PlanetDescriptorRepo : MongoRepository<PlanetDescriptor>, IPlanetDescriptorRepo
{
   public PlanetDescriptorRepo(IMongoContext? mongoContext,
      ILogger<PlanetDescriptorRepo> logger) : base(mongoContext, logger)
   {
   }

   public override async Task SeedDataAsync(CancellationToken cancellationToken = default)
   {
      long count = await Collection
         .CountDocumentsAsync(FilterDefinition<PlanetDescriptor>.Empty, cancellationToken: cancellationToken)
         .ConfigureAwait(false);
      if (count > 0)
         return;

      var descriptors = new List<PlanetDescriptor>
      {
         new()
         {
            PlanetType = PlanetType.Desert,
            Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable},
            Radius = new MinMax<int>(2000, 8000),
            Distance = new MinMax<double>(0.3, 0.7)
         },
         new()
         {
            PlanetType = PlanetType.GasGiant,
            Zones = new[] {PlanetaryZone.Outer},
            Radius = new MinMax<int>(35000, 800000),
            Distance = new MinMax<double>(4.0, 15.0)
         },
         new()
         {
            PlanetType = PlanetType.IceGiant,
            Zones = new[] {PlanetaryZone.Outer},
            Radius = new MinMax<int>(10000, 40000),
            Distance = new MinMax<double>(4.0, 15.0)
         },
         new()
         {
            PlanetType = PlanetType.Ice,
            Zones = new[] {PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable},
            Radius = new MinMax<int>(2000, 5000),
            Distance = new MinMax<double>(0.3, 0.7)
         },
         new()
         {
            PlanetType = PlanetType.Lava,
            Zones = new[] {PlanetaryZone.Inner, PlanetaryZone.InnerHabitable},
            Radius = new MinMax<int>(1500, 6000),
            Distance = new MinMax<double>(0.2, 0.6)
         },
         new()
         {
            PlanetType = PlanetType.Oceanic,
            Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable},
            Radius = new MinMax<int>(2000, 8000),
            Distance = new MinMax<double>(0.3, 0.7)
         },
         new()
         {
            PlanetType = PlanetType.Terrestrial,
            Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable},
            Radius = new MinMax<int>(2000, 8000),
            Distance = new MinMax<double>(0.3, 0.7)
         }
      };

      await InsertManyAsync(descriptors).ConfigureAwait(false);
   }
}
