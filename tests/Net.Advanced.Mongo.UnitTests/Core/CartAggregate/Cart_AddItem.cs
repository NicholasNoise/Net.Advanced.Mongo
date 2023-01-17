using Net.Advanced.Mongo.Core.CartAggregate;
using Xunit;

namespace Net.Advanced.Mongo.UnitTests.Core.CartAggregate;

public class Cart_AddItem
{
  private Cart _testCart = new();

  [Fact]
  public void AddsItemToItems()
  {
    // Arrange.
    var testItem = new CartItem(1, "CPU", null, 1m, 1);

    // Act.
    _testCart.AddItem(testItem);

    // Assert.
    Assert.Contains(testItem, _testCart.Items);
  }
}
