using BlueHarvest.Core.Utilities;
using FluentAssertions;

namespace BlueHarvest.Core.Tests;

public class RequestedAmountTests
{
   [Test]
   public void WhenCreated_WithExactAmount_GetAmount_WillReturn_ExactAmount()
   {
      var sut = new SystemAmount(10);

      var amount = sut.GetAmount();

      amount.Should().Be(10);
   }

   [Test]
   public void WhenCreated_WithRange_GetAmount_WillReturn_AmountInRange()
   {
      var sut = new SystemAmount(1, 10);

      var amount = sut.GetAmount();

      amount.Should().BeInRange(1, 10);
   }
}
