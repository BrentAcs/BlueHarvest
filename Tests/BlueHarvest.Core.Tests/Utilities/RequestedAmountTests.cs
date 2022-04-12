using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Tests;

public class RequestedAmountTests
{
   [Test]
   public void WhenCreated_WithExactAmount_GetAmount_WillReturn_ExactAmount()
   {
      var sut = new RequestedAmount(10);

      var amount = sut.GetAmount();

      Assert.AreEqual(10, amount);
   }

   [Test]
   public void WhenCreated_WithRange_GetAmount_WillReturn_AmountInRange()
   {
      var sut = new RequestedAmount(1, 10);

      var amount = sut.GetAmount();

      Assert.That(amount, Is.InRange(1, 10));
   }
}
