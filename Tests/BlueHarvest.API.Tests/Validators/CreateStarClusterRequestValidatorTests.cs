using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.API.Validators;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Storage.Repos;
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

   private CreateStarClusterRequestValidator CreateValidator(Mock<IStarClusterRepo>? repoMock = null)
   {
      if (repoMock is null)
      {
         var mockCursor = CreateCursorMock<StarCluster>();
         repoMock = new Mock<IStarClusterRepo>();
         repoMock.Setup(m => m.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);
      }

      return new CreateStarClusterRequestValidator(repoMock.Object);
   }

   private static IEnumerable<TestCaseData> ValidCreateStarClusterRequest()
   {
      yield return new TestCaseData(new CreateStarClusterRequest
      {
         Name = "test",
         Description = "test-description",
         Owner = "test-owner",
         ClusterSize = new Ellipsoid(100,100,50),
         DistanceBetweenSystems = new MinMax<double>(3, 10)
      });
   }

   [Test]
   [TestCaseSource(nameof(ValidCreateStarClusterRequest))]
   public void ValidateResult_IsValid_WillBe_True(CreateStarClusterRequest request)
   {
      var validator = CreateValidator();
      var result= validator.Validate(request);

      foreach (var error in result.Errors)
         TestContext.WriteLine($"{error.Severity}: {error.ErrorMessage}");

      Assert.IsTrue(result.IsValid);
   }

   private static IEnumerable<TestCaseData> InValidCreateStarClusterRequest()
   {
      yield return new TestCaseData(new CreateStarClusterRequest { });
      yield return new TestCaseData(new CreateStarClusterRequest
      {
         Name = "test",
         Description = "test-description",
         Owner = "test-owner",
         ClusterSize = new Ellipsoid(101,101,51),
         DistanceBetweenSystems = new MinMax<double>(2, 11)
      });
   }

   [Test]
   [TestCaseSource(nameof(InValidCreateStarClusterRequest))]
   public void ValidateResult_IsValid_WillBe_False(CreateStarClusterRequest request)
   {
      var validator = CreateValidator();
      var result= validator.Validate(request);

      foreach (var error in result.Errors)
         TestContext.WriteLine($"{error.Severity}: {error.ErrorMessage}");

      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBe_False_WhenName_Exists()
   {
      var repoMock = new Mock<IStarClusterRepo>();
      var cursorMock = CreateCursorMock( new []{new StarCluster{Name = "Test"}});
      repoMock = new Mock<IStarClusterRepo>();
      repoMock.Setup(m => m.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(cursorMock.Object);
      var validator = CreateValidator(repoMock);

      var result= validator.Validate(new CreateStarClusterRequest
      {
         Name = "Test"
      });

      Assert.IsFalse(result.IsValid);
   }
}
