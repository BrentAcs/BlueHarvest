using System.Linq;
using BlueHarvest.API.Actions.Cosmic;
using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.API.Tests;

public class GetAllStarClustersTests
{
   private readonly IMapper _mapper;
   private IFixture _fixture;

   public GetAllStarClustersTests()
   {
      _mapper = Utilities.Mapper;
      _fixture = new Fixture();
      _fixture.Register(() => ObjectId.Empty);
   }
   
   [SetUp]
   public void Setup()
   {
   }
   
   [Test]
   public async Task QueryOnHandler_WillReturn_MappedDto()
   {
      var loggerMock = new Mock<ILogger<GetAllStarClusters.Query>>();
      var repoMock = new Mock<IStarClusterRepo>();
      var cluster = _fixture.Create<StarCluster>();
      repoMock.Setup(m => m.All()).Returns(new List<StarCluster> {cluster});
      var sut = new GetAllStarClusters.Query(loggerMock.Object, _mapper, repoMock.Object);

      var response = await sut.Handle(GetAllStarClusters.Default, CancellationToken.None);

      response.Data.Should().NotBeEmpty();
      response.Data.First().Name.Should().Be(cluster.Name);
   }
}
