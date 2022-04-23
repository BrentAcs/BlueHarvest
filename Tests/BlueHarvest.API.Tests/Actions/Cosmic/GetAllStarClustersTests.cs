using System.Linq;
using BlueHarvest.API.Actions.Cosmic;
using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.API.Tests;

public class GetAllStarClustersTests
{
   private readonly IFixture _fixture;

   public GetAllStarClustersTests()
   {
      _fixture = Utilities.GetSimpleFixture();
   }
   
   [SetUp]
   public void Setup()
   {
   }
   
   [Test]
   public async Task QueryOnHandler_WillReturn_MappedDto()
   {
      var repoMock = new Mock<IStarClusterRepo>();
      var cluster = _fixture.Create<StarCluster>();
      repoMock.Setup(m => m.All()).Returns(new List<StarCluster> {cluster});
      var sut = Utilities.CreateGetAllStarClustersQuery(repoMock);

      var response = await sut.Handle(GetAllStarClusters.Default, CancellationToken.None);

      response.Data.Should().NotBeEmpty();
      response.Data.First().Name.Should().Be(cluster.Name);
   }
}
