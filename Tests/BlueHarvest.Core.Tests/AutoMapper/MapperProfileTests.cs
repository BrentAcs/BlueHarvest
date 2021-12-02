using BlueHarvest.Core.AutoMapper;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Commands.Cosmic;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Utilities;
using MongoDB.Bson;

namespace BlueHarvest.API.Tests.DTOs;

[TestFixture]
public class AutoMapperProfileTests
{
   private static IMapper Mapper =>
      new MapperConfiguration(cfg =>
      {
         cfg.AddProfile(typeof(MapperProfile));
      }).CreateMapper();

   [Test]
   public void WillMap_CreateStarClusterRequest_To_StarClusterBuilderOptions()
   {
      var dto = new CreateStarCluster
      {
         Name = "test-name",
         Description = "test-description",
         Owner = "test-owner",
         ClusterSize = new Ellipsoid(10, 20, 30),
         DistanceBetweenSystems = new MinMax<double>(42, 69),
         PlanetarySystemOptions = new PlanetarySystemBuilderOptions
         {
            SystemRadius = new MinMax<double>(69, 42), DistanceMultiplier = 1.2345
         }
      };

      var options = Mapper.Map<StarClusterBuilderOptions>(dto);

      StringAssert.AreEqualIgnoringCase("test-name", options.Name);
      StringAssert.AreEqualIgnoringCase("test-description", options.Description);
      StringAssert.AreEqualIgnoringCase("test-owner", options.Owner);
      Assert.AreEqual(10, options.ClusterSize.XRadius);
      Assert.AreEqual(20, options.ClusterSize.YRadius);
      Assert.AreEqual(30, options.ClusterSize.ZRadius);
      Assert.AreEqual(42, options.DistanceBetweenSystems.Min);
      Assert.AreEqual(69, options.DistanceBetweenSystems.Max);
      Assert.AreEqual(69, options.SystemOptions.SystemRadius.Min);
      Assert.AreEqual(42, options.SystemOptions.SystemRadius.Max);
      Assert.AreEqual(1.2345, options.SystemOptions.DistanceMultiplier);
   }

   [Test]
   public void WillMap_StarCluster_To_StarClusterResponse()
   {
      var createdOn = DateTime.Now;
      var cluster = new StarCluster
      {
         Id = ObjectId.Empty,
         Name = "test-name",
         Description = "test-description",
         Owner = "test-owner",
         CreatedOn = createdOn,
         Size = new Sphere(11)
      };

      var dto = Mapper.Map<StarClusterResponse>(cluster);

      StringAssert.AreEqualIgnoringCase("test-name", dto.Name);
      StringAssert.AreEqualIgnoringCase("test-description", dto.Description);
      StringAssert.AreEqualIgnoringCase("test-owner", dto.Owner);
      Assert.AreEqual(createdOn, dto.CreatedOn);
      Assert.AreEqual(11, dto.Size.XRadius);
      Assert.AreEqual(11, dto.Size.YRadius);
      Assert.AreEqual(11, dto.Size.ZRadius);
   }
}
