using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Validators;

namespace BlueHarvest.Core.Tests.Validators;

[TestFixture]
public class EllipsoidValidatorTests
{
   public class TestEllipsoidValidator<T> : EllipsoidValidator<T>
   {
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_Ellipsoid_XRadius_IsZero()
   {
      var validator = new TestEllipsoidValidator<int>();
      var result = validator.Validate(new Ellipsoid(0, 10, 10));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_Ellipsoid_XRadius_IsLessThanZero()
   {
      var validator = new TestEllipsoidValidator<int>();
      var result = validator.Validate(new Ellipsoid(-10, 10, 10));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_Ellipsoid_YRadius_IsZero()
   {
      var validator = new TestEllipsoidValidator<int>();
      var result = validator.Validate(new Ellipsoid(10, 0, 10));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_Ellipsoid_YRadius_IsLessThanZero()
   {
      var validator = new TestEllipsoidValidator<int>();
      var result = validator.Validate(new Ellipsoid(10, -10, 10));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_Ellipsoid_ZRadius_IsZero()
   {
      var validator = new TestEllipsoidValidator<int>();
      var result = validator.Validate(new Ellipsoid(10, 10, -10));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_Ellipsoid_ZRadius_IsLessThanZero()
   {
      var validator = new TestEllipsoidValidator<int>();
      var result = validator.Validate(new Ellipsoid(10, 10, -10));
      Assert.IsFalse(result.IsValid);
   }
}
