using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Tests.Misc;

[TestFixture]
public class MinMaxTests
{
   [Test]
   public void CanSerialize_MinMax_OfInt()
   {
      var mm = new MinMax<int>(10, 20);
      var json = mm.AsJson();
      var mm2 = json.FromJson<MinMax<int>>();
      
      Assert.AreEqual(mm.Min, mm2.Min);
      Assert.AreEqual(mm.Max, mm2.Max);
   }
}
