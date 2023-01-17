using Net.Advanced.Mongo.SharedKernel;

namespace Net.Advanced.Mongo.Core.CartAggregate.Events;

public class ChangedItemQuantityEvent : DomainEventBase
{
  public Cart Cart { get; set; }

  public CartItem ChangedItem { get; set; }

  public ChangedItemQuantityEvent(
    Cart cart,
    CartItem changedItem)
  {
    Cart = cart;
    ChangedItem = changedItem;
  }
}
