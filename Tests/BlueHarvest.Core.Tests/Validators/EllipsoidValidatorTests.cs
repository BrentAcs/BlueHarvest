using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Validators;

namespace BlueHarvest.Core.Tests.Validators;

[TestFixture]
public class EllipsoidValidatorTests
{
   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_Validator_IsInitializedWithNullValues()
   {
      var validator = new EllipsoidValidator(null, null, null);
      var result = validator.Validate(new Ellipsoid(9, 19, 29));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_ValidatingXRadius_IsLessThan()
   {
      var validator = new EllipsoidValidator(10, null, null);
      var result = validator.Validate(new Ellipsoid(9, 9, 9));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_ValidatingYRadius_IsLessThan()
   {
      var validator = new EllipsoidValidator(null, 10, null);
      var result = validator.Validate(new Ellipsoid(9, 9, 9));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_ValidatingZRadius_IsLessThan()
   {
      var validator = new EllipsoidValidator(null, null, 10);
      var result = validator.Validate(new Ellipsoid(9, 9, 9));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_ValidatingXRadius_IsEqualTo()
   {
      var validator = new EllipsoidValidator(10, null, null);
      var result = validator.Validate(new Ellipsoid(10, 10, 10));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_ValidatingYRadius_IsEqualTo()
   {
      var validator = new EllipsoidValidator(null, 10, null);
      var result = validator.Validate(new Ellipsoid(10, 10, 10));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeTrue_When_ValidatingZRadius_IsEqualTo()
   {
      var validator = new EllipsoidValidator(null, null, 10);
      var result = validator.Validate(new Ellipsoid(10, 10, 10));
      Assert.IsTrue(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_ValidatingXRadius_IsGreaterThan()
   {
      var validator = new EllipsoidValidator(10, null, null);
      var result = validator.Validate(new Ellipsoid(11, 11, 11));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_ValidatingYRadius_IsGreaterThan()
   {
      var validator = new EllipsoidValidator(null, 10, null);
      var result = validator.Validate(new Ellipsoid(11, 11, 11));
      Assert.IsFalse(result.IsValid);
   }

   [Test]
   public void ValidateResult_IsValid_WillBeFalse_When_ValidatingZRadius_IsGreaterThan()
   {
      var validator = new EllipsoidValidator(null, null, 10);
      var result = validator.Validate(new Ellipsoid(11, 11, 11));
      Assert.IsFalse(result.IsValid);
   }
}
