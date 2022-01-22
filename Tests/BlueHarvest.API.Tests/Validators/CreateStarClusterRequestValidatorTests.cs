using BlueHarvest.Core.Actions.Cosmic;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Core.Validators;
using MongoDB.Driver;

namespace BlueHarvest.API.Tests.Validators;

[TestFixture]
public class CreateStarClusterRequestValidatorTests
{
   private Mock<IAsyncCursor<T>> CreateCursorMock<T>(IEnumerable<T>? items = null)
   {
      items ??= new List<T>();

      var cursorMock = new Mock<IAsyncCursor<T>>();
      cursorMock.Setup(_ => _.Current).Returns(items); //<-- Note the entities here
      cursorMock
         .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
         .Returns(true)
         .Returns(false);
      cursorMock
         .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
         .Returns(Task.FromResult(true))
         .Returns(Task.FromResult(false));

      return cursorMock;
   }

   private CreateStarClusterDtoValidator CreateValidator(Mock<IStarClusterRepo>? repoMock = null)
   {
      if (repoMock is null)
      {
         var mockCursor = CreateCursorMock<StarCluster>();
         repoMock = new Mock<IStarClusterRepo>();
         repoMock.Setup(m => m.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);
      }

      return new CreateStarClusterDtoValidator(repoMock.Object);
   }

   private static IEnumerable<TestCaseData> ValidCreateStarClusterRequest()
   {
      yield return new TestCaseData(new CreateStarClusterDto()
      {
         Name = "test",
         Description = "test-description",
         Owner = "test-owner",
         ClusterSize = new Ellipsoid(100, 100, 50),
         DistanceBetweenSystems = new MinMax<double>(3, 10)
      });
   }

   [Test]
   [TestCaseSource(nameof(ValidCreateStarClusterRequest))]
   public void ValidateResult_IsValid_WillBe_True(CreateStarClusterDto dto)
   {
      var validator = CreateValidator();
      var result = validator.Validate(dto);

      foreach (var error in result.Errors)
         TestContext.WriteLine($"{error.Severity}: {error.ErrorMessage}");

      Assert.IsTrue(result.IsValid);
   }

   private static IEnumerable<TestCaseData> InValidCreateStarClusterRequest()
   {
      yield return new TestCaseData(new CreateStarClusterDto { });
      yield return new TestCaseData(new CreateStarClusterDto
      {
         Name = "test",
         Description = "test-description",
         Owner = "test-owner",
         ClusterSize = new Ellipsoid(101, 101, 51),
         DistanceBetweenSystems = new MinMax<double>(2, 11)
      });
   }

   [Test]
   [TestCaseSource(nameof(InValidCreateStarClusterRequest))]
   public void ValidateResult_IsValid_WillBe_False(CreateStarClusterDto dto)
   {
      var validator = CreateValidator();
      var result = validator.Validate(dto);

      foreach (var error in result.Errors)
         TestContext.WriteLine($"{error.Severity}: {error.ErrorMessage}");

      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBe_False_WhenName_Exists()
   {
      var repoMock = new Mock<IStarClusterRepo>();
      var cursorMock = CreateCursorMock(new[] {new StarCluster {Name = "Test"}});
      repoMock = new Mock<IStarClusterRepo>();
      repoMock.Setup(m => m.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(cursorMock.Object);
      var validator = CreateValidator(repoMock);

      var result = validator.Validate(new CreateStarClusterDto {Name = "Test"});

      Assert.IsFalse((bool)result.IsValid);
   }
}
