using System.Reflection.PortableExecutable;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Utilities;
using SharpCompress.Archives;

namespace BlueHarvest.Core.Services.Factories;

public interface IPlanetaryDistanceFactory
{
   IEnumerable<double> Create(double maxDistance);
}

public class PlanetaryDistanceFactory : IPlanetaryDistanceFactory
{
   private readonly IRng _rng;
   // | Zone      | Distance Range (AU) | # (Range) | (Min Distance) |
   // | --------- |---------------------|-----------|----------------|
   // | Inner     | .1  - .25           | 0 - 1     | .1             |
   // | Inner-Hab | .25  - 1.5          | 0 - 2     | .35            |
   // | Habitable | 1.5  - 3.5          | 0 - 2     | .35            |     
   // | Outer-Hab | 3.5 - 5.0           | 0 - 2     | .35            |
   // | Outer     | 5.0  - ~            | 2 - 10    | 1              |         

   private class PlanetaryZoneDescriptor
   {
      public PlanetaryZoneDescriptor(PlanetaryZone zone, MinMax<double> distanceRange, MinMax<int> numberPossible, double minDistance)
      {
         Zone = zone;
         DistanceRange = distanceRange;
         NumberPossible = numberPossible;
         MinDistance = minDistance;
      }

      public PlanetaryZone Zone { get; }
      public MinMax<double> DistanceRange { get; }
      public MinMax<int> NumberPossible { get; }
      public double MinDistance { get; }
   }

   private readonly List<PlanetaryZoneDescriptor> _zoneDescriptors = new()
   {
      new PlanetaryZoneDescriptor(PlanetaryZone.Inner, new MinMax<double>(.1, .2499), new MinMax<int>(0, 1), .1),
      new PlanetaryZoneDescriptor(PlanetaryZone.InnerHabitable, new MinMax<double>(.25, 1.4999), new MinMax<int>(0, 2), .35),
      new PlanetaryZoneDescriptor(PlanetaryZone.Habitable, new MinMax<double>(1.5, 3.4999), new MinMax<int>(1, 2), .35),
      new PlanetaryZoneDescriptor(PlanetaryZone.OuterHabitable, new MinMax<double>(3.5, 4.9999), new MinMax<int>(0, 2), .35),
      new PlanetaryZoneDescriptor(PlanetaryZone.Outer, new MinMax<double>(5, double.MaxValue), new MinMax<int>(2, 15), 1.0),
   };

   public PlanetaryDistanceFactory(IRng rng)
   {
      _rng = rng;
   }

   public IEnumerable<double> Create(double maxDistance)
   {
      var distances = new List<double>();

      foreach (var descriptor in _zoneDescriptors)
      {
         double zoneStartDistance = descriptor.DistanceRange.Min;
         var zoneDistances = new List<double>();
         double max = descriptor.DistanceRange.Max == double.MaxValue ? maxDistance : descriptor.DistanceRange.Max;
         int count = _rng.Next(descriptor.NumberPossible!);
         for (int i = 0; i < count; i++)
         {
            double distance = _rng.Next(descriptor.DistanceRange.Min, max);
            if (!zoneDistances.Any())
            {
               zoneDistances.Add(+zoneStartDistance + distance);
            }
            else
            {
               for (int attempt = 0; attempt < 5; attempt++)
               {
                  if (zoneDistances.Any(d => Math.Abs(distance - d) > descriptor.MinDistance))
                  {
                     zoneDistances.Add(zoneStartDistance + distance);
                     break;
                  }
                  distance = _rng.Next(descriptor.DistanceRange.Min, max);
               }
            }
         }

         distances.AddRange(zoneDistances);
      }

      return distances.OrderBy(d => d);
   }
}
