using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Factories;

public interface IPlanetarySystemFactory
{
   PlanetarySystem Create(PlanetarySystemFactoryOptions options, ObjectId clusterId, Point3D location);
}
