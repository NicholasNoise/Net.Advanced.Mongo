using Net.Advanced.Mongo.SharedKernel;

namespace Net.Advanced.Mongo.Core.CartAggregate.Events;

public class ItemDeletedEvent : DomainEventBase
{
  public Cart Cart { get; set; }

  public CartItem DeletedItem { get; set; }

  public ItemDeletedEvent(
    Cart cart,
    CartItem deletedItem)
  {
    Cart = cart;
    DeletedItem = deletedItem;
  }
}
