using Net.Advanced.Mongo.SharedKernel;

namespace Net.Advanced.Mongo.Core.CartAggregate.Events;

public class NewItemAddedEvent : DomainEventBase
{
  public Cart Cart { get; set; }

  public CartItem NewItem { get; set; }

  public NewItemAddedEvent(
    Cart cart,
    CartItem newItem)
  {
    Cart = cart;
    NewItem = newItem;
  }
}
