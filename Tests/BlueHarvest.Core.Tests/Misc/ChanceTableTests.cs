using BlueHarvest.Core.Misc;

namespace BlueHarvest.Core.Tests.Misc;

[TestFixture]
public class ChanceTableTests
{
   [Test]
   public void Count_WillReturn1_AfterAddIsCalledOnce()
   {
      var col = new ChanceTable<string>();
      col.Add("brent", 10.0);

      Assert.AreEqual(1, col.Count);
   }

   [Test]
   public void Count_WillReturn0_AfterClearIsCalled()
   {
      var col = new ChanceTable<string>();
      col.Add("brent", 10.0);
      col.Clear();

      Assert.AreEqual(0, col.Count);
   }

   [Test]
   public void Contains_WillReturnFalse_IfItemHasNotBeenAdded()
   {
      var col = new ChanceTable<string>();

      Assert.IsFalse(col.Contains("brent"));
   }

   [Test]
   public void Contains_WillReturnTrue_IfItemHasBeenAdded()
   {
      var col = new ChanceTable<string>();
      col.Add("brent");

      Assert.IsTrue(col.Contains("brent"));
   }

   [Test]
   public void PercentTotal_WillReturn_ValidTotal()
   {
      var col = new ChanceTable<string>();
      col.Add("brent", 10.0);
      col.Add("kitty", 20.0);

      Assert.That(30.0, Is.EqualTo(col.PercentTotal));
   }

   [TestCase(4.0, ExpectedResult = "a")]
   public string GetItem_WillReturn_ProperValue(double roll)
   {
      var col = new ChanceTable<string>();
      col.Add("a", 5.0);      // translates to [0 - 5]
      col.Add("b", 10.0);     // translates to (5 - 15]
      col.Add("c", 25.0);     // translates to (15 - 40]
      col.Add("e"); // translates to

      return col.GetItem(roll);
   }


   [Test]
   public void Add_WillThrowArgumentException_IfItemExists()
   {
      var col = new ChanceTable<string>();
      col.Add("brent");

      var ex = Assert.Throws<ArgumentException>(() => col.Add("brent"));
      Assert.That(ex?.Message, Is.EqualTo("item already exists in collection."));
   }

   [Test]
   public void Add_WillThrowArgumentException_IfItemAnotherItemExistsWithDefaultChance()
   {
      var col = new ChanceTable<string>();
      col.Add("brent");

      var ex = Assert.Throws<ArgumentException>(() => col.Add("kitty"));
      Assert.That(ex?.Message, Is.EqualTo("item with default chance exists in collection."));
   }

   [Test]
   public void Add_WillThrowArgumentException_IfItemAddedExceeds100Chance()
   {
      var col = new ChanceTable<string>();
      col.Add("brent", 50);
      col.Add("kitty", 25);

      var ex = Assert.Throws<ArgumentException>(() => col.Add("connor", 50));
      Assert.That(ex?.Message, Is.EqualTo("item chance will exceed 100%."));
   }
}
