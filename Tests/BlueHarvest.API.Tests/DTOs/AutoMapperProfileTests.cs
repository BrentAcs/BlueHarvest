using BlueHarvest.API.DTOs;
using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.API.Tests.DTOs;

[TestFixture]
public class AutoMapperProfileTests
{
   private static IMapper Mapper =>
      new MapperConfiguration(cfg =>
      {
         cfg.AddProfile(typeof(AutoMapperProfile));
      }).CreateMapper();

   [Test]
   public void WillMap_CreateStarClusterRequestDto_To_StarClusterBuilderOptions()
   {
      var dto = new CreateStarClusterRequestDto
      {
         Name = "test-name",
         Description = "test-description",
         Owner = "test-owner",
         ClusterSize = new Ellipsoid(10, 20, 30),
         SystemDistance = new MinMax<double>(42, 69)
      };

      var options = Mapper.Map<StarClusterBuilderOptions>(dto);

      StringAssert.AreEqualIgnoringCase("test-name", options.Name);
      StringAssert.AreEqualIgnoringCase("test-description", options.Description);
      StringAssert.AreEqualIgnoringCase("test-owner", options.Owner);
      Assert.AreEqual(10, options.ClusterSize.XRadius);
      Assert.AreEqual(20, options.ClusterSize.YRadius);
      Assert.AreEqual(30, options.ClusterSize.ZRadius);
      Assert.AreEqual(42, options.SystemDistance.Min);
      Assert.AreEqual(69, options.SystemDistance.Max);
   }

   [Test]
   public void WillMap_StarCluster_To_CreateStarClusterResponseDto()
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

      var dto = Mapper.Map<CreateStarClusterResponseDto>(cluster);

      StringAssert.AreEqualIgnoringCase("test-name", dto.Name);
      StringAssert.AreEqualIgnoringCase("test-description", dto.Description);
      StringAssert.AreEqualIgnoringCase("test-owner", dto.Owner);
      Assert.AreEqual(createdOn, dto.CreatedOn);
      Assert.AreEqual(11, dto.Size.XRadius);
      Assert.AreEqual(11, dto.Size.YRadius);
      Assert.AreEqual(11, dto.Size.ZRadius);
   }
}
