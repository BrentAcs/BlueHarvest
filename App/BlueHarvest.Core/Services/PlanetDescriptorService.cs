using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services;

public interface IPlanetDescriptorService
{
   PlanetaryZone IdentifyPlanetaryZone(double distance);
   PlanetType GeneratePlanetType(PlanetaryZone zone);
   double GenerateDiameter(PlanetType planetType);
   double GenerateNextDistance(PlanetType planetType);
}

public class PlanetDescriptorService : IPlanetDescriptorService
{
   private record PlanetDescriptor
   {
      public PlanetaryZone[]? Zones { get; set; }
      public MinMax<int>? Radius { get; set; }
      public MinMax<double>? Distance { get; set; }
   }

   private readonly IRng _rng;

   public PlanetDescriptorService(IRng rng)
   {
      _rng = rng;
   }

   private readonly IDictionary<PlanetType, PlanetDescriptor> _planetDescriptors =
      new Dictionary<PlanetType, PlanetDescriptor>
      {
         {
            PlanetType.Desert,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable },
               Radius = new MinMax<int>(2000, 8000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
         {
            PlanetType.GasGiant,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Outer },
               Radius = new MinMax<int>(35000, 800000),
               Distance = new MinMax<double>(4.0, 15.0)
            }
         },
         {
            PlanetType.IceGiant,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Outer },
               Radius = new MinMax<int>(10000, 40000),
               Distance = new MinMax<double>(4.0, 15.0)
            }
         },
         {
            PlanetType.Ice,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable },
               Radius = new MinMax<int>(2000, 5000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
         {
            PlanetType.Lava,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Inner, PlanetaryZone.InnerHabitable },
               Radius = new MinMax<int>(1500, 6000),
               Distance = new MinMax<double>(0.2, 0.6)
            }
         },
         {
            PlanetType.Oceanic,
            new PlanetDescriptor
            {
               Zones = new[]
               {
                  PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable
               },
               Radius = new MinMax<int>(2000, 8000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
         {
            PlanetType.Terrestrial,
            new PlanetDescriptor
            {
               Zones = new[]
               {
                  PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable
               },
               Radius = new MinMax<int>(2000, 8000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
      };

   public PlanetaryZone IdentifyPlanetaryZone(double distance) =>
      distance switch
      {
         <= 0.2499 => PlanetaryZone.Inner,
         <= 1.499 => PlanetaryZone.InnerHabitable,
         <= 3.499 => PlanetaryZone.Habitable,
         <= 4.999 => PlanetaryZone.OuterHabitable,
         _ => PlanetaryZone.Outer
      };

   public PlanetType GeneratePlanetType(PlanetaryZone zone)
   {
      var planetTypes = _planetDescriptors
         .Where(p => p.Value.Zones.Contains(zone))
         .Select(p => p.Key)
         .ToList();

      return planetTypes[ _rng.Next(0, planetTypes.Count - 1) ];
   }

   public double GenerateDiameter(PlanetType planetType) =>
      _rng.Next(_planetDescriptors[ planetType ].Radius) * 2;

   public double GenerateNextDistance(PlanetType planetType) =>
      _rng.Next(_planetDescriptors[ planetType ].Distance);
}
