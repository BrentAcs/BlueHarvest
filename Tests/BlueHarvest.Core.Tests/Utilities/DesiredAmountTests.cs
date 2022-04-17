using BlueHarvest.Core.Utilities;
using FluentAssertions;

namespace BlueHarvest.Core.Tests;

public class DesiredAmountTests
{
   [Test]
   public void WhenCreated_WithExactAmount_GetAmount_WillReturn_ExactAmount()
   {
      var sut = new DesiredAmount(10);

      var amount = sut.GetAmount();

      amount.Should().Be(10);
   }

   [Test]
   public void WhenCreated_WithRange_GetAmount_WillReturn_AmountInRange()
   {
      var sut = new DesiredAmount(1, 10);

      var amount = sut.GetAmount();

      amount.Should().BeInRange(1, 10);
   }
}
