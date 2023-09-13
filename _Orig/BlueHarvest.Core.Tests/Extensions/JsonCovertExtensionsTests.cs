using BlueHarvest.Core.Extensions;

namespace BlueHarvest.Core.Tests.Extensions;

[TestFixture]
public class JsonCovertExtensionsTests
{
   public class SamplePoco
   {
      public string AString { get; set; }
   }

   [Test]
   public void WhenObject_IsNull_ToJson_ReturnsNull()
   {
      var test = ((SamplePoco)null).AsJson();

      Assert.IsNull(test);
   }

   [Test]
   public void WhenObject_IsNotNull_ToJson_ReturnsNoneEmptyString()
   {
      var test = new SamplePoco {AString = "AValue"}.AsJson();

      Assert.IsNotEmpty(test);
   }

   [Test]
   public void WhenObject_IsNotNull_ToJsonIndented_ReturnsNoneEmptyString()
   {
      var test = new SamplePoco {AString = "AValue"}.AsJsonIndented();

      Assert.IsNotEmpty(test);
   }

   [Test]
   public void WhenString_IsValidJson_FromJson_ReturnsNoneNull()
   {
      var test = "{ \"AString\": \"AValue\" }".FromJson<SamplePoco>();

      Assert.NotNull(test);
   }

   [Test]
   public void WhenString_IsInvalidValidJson_FromJson_ReturnsNull()
   {
      Assert.Throws<JsonReaderException>(() =>
      {
         "{ not even remotely valid json }".FromJson<SamplePoco>();
      });
   }
}
