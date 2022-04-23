using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Models.Geometry;

namespace BlueHarvest.Core.Services.Factories;

public interface IPlanetarySystemFactory
{
   Task<PlanetarySystem> Create(PlanetarySystemFactoryOptions options, ObjectId clusterId, Point3D location);
}
