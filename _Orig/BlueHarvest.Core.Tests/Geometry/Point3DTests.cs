using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Tests.Geometry;

[TestFixture]
public class Point3DTests
{
   private static IEnumerable<TestCaseData> EquivalentPoints()
   {
      yield return new TestCaseData(new Point3D(1, 1, 1), new Point3D(1, 1, 1));
      yield return new TestCaseData(new Point3D(10, 10, 10), new Point3D(10, 10, 10));
   }

   [Test]
   [TestCaseSource(nameof(EquivalentPoints))]
   public void OperatorEqual_WillBe_True(Point3D lhs, Point3D rhs) =>
      Assert.IsTrue(lhs == rhs);
   
   private static IEnumerable<TestCaseData> UnequivalentPoints()
   {
      yield return new TestCaseData(new Point3D(1.1, 1.1, 1.1), new Point3D(1, 1, 1));
      yield return new TestCaseData(new Point3D(10.1, 10.1, 10.1), new Point3D(10, 10, 10));
   }

   [Test]
   [TestCaseSource(nameof(UnequivalentPoints))]
   public void OperatorEqual_WillBe_False(Point3D lhs, Point3D rhs) =>
      Assert.IsTrue(lhs != rhs);
   
}
