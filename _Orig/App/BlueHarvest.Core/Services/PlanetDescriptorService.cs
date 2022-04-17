using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services;

public interface IPlanetDescriptorService
{
   PlanetaryZone IdentifyPlanetaryZone(double distance);
   PlanetType GeneratePlanetType(PlanetaryZone zone);
   PlanetDescriptor GetRandomPlanetDescriptor(PlanetaryZone zone);
}

public class PlanetDescriptorService : IPlanetDescriptorService
{
   private readonly IPlanetDescriptorRepo _repo;
   private readonly IRng _rng;
   private IEnumerable<PlanetDescriptor>? _descriptors = null;

   public PlanetDescriptorService(IPlanetDescriptorRepo repo, IRng rng)
   {
      _repo = repo;
      _rng = rng;
   }

   private IEnumerable<PlanetDescriptor> Descriptors
   {
      get
      {
         return _descriptors ??= _repo.All();
      }
   }

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
      var planetTypes = Descriptors?
         .Where(d => d?.Zones != null && d.Zones.Contains(zone))
         .Select(d => d.PlanetType)
         .ToList();

      return planetTypes![_rng.Next(0, planetTypes.Count - 1)];
   }

   public PlanetDescriptor GetRandomPlanetDescriptor(PlanetaryZone zone)
   {
      var range = Descriptors?
         .Where(d => d?.Zones != null && d.Zones.Contains(zone))
         .ToList();

      return range![_rng.Next(0, range.Count - 1)];
   }
}
