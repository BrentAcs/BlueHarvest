using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Storage.Repos;

public interface IStarDescriptorRepo : IMongoRepository<StarDescriptor>
{
}
public class StarDescriptorRepo : MongoRepository<StarDescriptor>, IStarDescriptorRepo
{
   public StarDescriptorRepo(IMongoContext mongoContext) : base(mongoContext)
   {
   }

   public override async Task SeedDataAsync()
   {
      long count = await Collection.CountDocumentsAsync(FilterDefinition<StarDescriptor>.Empty)
         .ConfigureAwait(false);
      if (count > 0)
         return;

      var descriptors = new List<StarDescriptor>
      {
         new()
         {
            StarType = StarType.ClassB,
            Name = "Blue",
            Chance = 0.625,
            Mass = new MinMax<double>(2.5, 90.0)
         },
         new()
         {
            StarType = StarType.ClassA,
            Name = "Blue Giant",
            Chance = 3.125,
            Mass = new MinMax<double>(2.0, 150.0)
         },
         new()
         {
            StarType = StarType.ClassF,
            Name = "White",
            Chance = 15.0,
            Mass = new MinMax<double>(0.8, 1.4)
         },
         new()
         {
            StarType = StarType.ClassG,
            Name = "Yellow Dwarf",
            Chance = 38.5,
            Mass = new MinMax<double>(0.7, 1.4)
         },
         new()
         {
            StarType = StarType.ClassK,
            Name = "Orange Dwarf",
            Chance = 41.5,
            Mass = new MinMax<double>(0.45, 0.8)
         },
      };

      await InsertManyAsync(descriptors).ConfigureAwait(false);
   }
}
