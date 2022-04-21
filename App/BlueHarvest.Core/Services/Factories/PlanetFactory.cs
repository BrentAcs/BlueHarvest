using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface IPlanetFactory
{
   Planet Create(double distance);
}

public class PlanetFactory : BaseFactory, IPlanetFactory
{
   private static readonly List<PlanetDescriptor> _planetDescriptors = new()
   {
      new PlanetDescriptor
      {
         PlanetType = PlanetType.Desert,
         Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable},
         Radius = new MinMax<int>(2000, 8000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.GasGiant,
         Zones = new[] {PlanetaryZone.Outer},
         Radius = new MinMax<int>(35000, 800000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.IceGiant,
         Zones = new[] {PlanetaryZone.Outer},
         Radius = new MinMax<int>(10000, 40000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.Ice,
         Zones = new[] {PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable},
         Radius = new MinMax<int>(2000, 5000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.Lava,
         Zones = new[] {PlanetaryZone.Inner, PlanetaryZone.InnerHabitable},
         Radius = new MinMax<int>(1500, 6000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.Oceanic,
         Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable},
         Radius = new MinMax<int>(2000, 8000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.Terrestrial,
         Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable},
         Radius = new MinMax<int>(2000, 8000),
      },
      new PlanetDescriptor
      {
         PlanetType = PlanetType.Barren,
         Zones = new[] {PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable, PlanetaryZone.Outer},
         Radius = new MinMax<int>(2000, 8000),
      }
   };

   private class PlanetDescriptor
   {
      public PlanetType PlanetType { get; set; }
      public PlanetaryZone[] Zones { get; set; }
      public MinMax<int> Radius { get; set; }
   }

   public PlanetFactory(IRng rng) : base(rng)
   {
   }

   public Planet Create(double distance)
   {
      var zone = distance.IdentifyPlanetaryZone();

      var potentials = _planetDescriptors.Where(d => d.Zones.Contains(zone)).ToList();
      var descriptor = potentials[ Rng.Next(0, potentials.Count()) ];

      var planet = new Planet
      {
         Type = descriptor.PlanetType,
         Zone = zone,
         Distance = distance,
         Diameter = Rng.Next(descriptor.Radius) * 2
      };

      return planet;
   }
}
