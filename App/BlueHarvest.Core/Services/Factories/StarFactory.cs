using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface IStarFactory
{
   Star Create();
}

public class StarFactory : IStarFactory
{
   private readonly IRng _rng;

   private readonly List<Descriptor> _descriptors = new List<Descriptor>
   {
      new()
      {
         StarType = Star.StarType.ClassB,
         Name = "Blue",
         Chance = 0.625,
         MassRange = new MinMax<double>(2.5, 90.0)
      },
      new()
      {
         StarType = Star.StarType.ClassA,
         Name = "Blue Giant",
         Chance = 3.125,
         MassRange = new MinMax<double>(2.0, 150.0)
      },
      new()
      {
         StarType = Star.StarType.ClassF,
         Name = "White",
         Chance = 15.0,
         MassRange = new MinMax<double>(0.8, 1.4)
      },
      new()
      {
         StarType = Star.StarType.ClassG,
         Name = "Yellow Dwarf",
         Chance = 38.5,
         MassRange = new MinMax<double>(0.7, 1.4)
      },
      new()
      {
         StarType = Star.StarType.ClassK,
         Name = "Orange Dwarf",
         Chance = 41.5,
         MassRange = new MinMax<double>(0.45, 0.8)
      },
   };

   private class Descriptor : IHaveChance
   {
      public Star.StarType StarType { get; set; }
      public string? Name { get; set; }
      public double Chance { get; set; }
      public MinMax<double>? MassRange { get; set; }
   }

   public StarFactory(IRng rng)
   {
      _rng = rng;
   }

   public Star Create()
   {
      var roll = _rng.Next(1.0, 101.0);
      var descriptor = _descriptors.GetByChance(roll);

      return new Star
      {
         Type = descriptor.StarType,
         Mass = _rng.Next(descriptor.MassRange!)
      };
   }
}
