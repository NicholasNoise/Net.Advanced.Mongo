using Ardalis.GuardClauses;
using Net.Advanced.Mongo.Core.CartAggregate.Events;
using Net.Advanced.Mongo.SharedKernel;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Core.CartAggregate;
public class Cart : EntityBase, IAggregateRoot
{
  public string? Name { get; set; }

  public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

  public void AddItem(CartItem newItem)
  {
    Guard.Against.Null(newItem, nameof(newItem));
    Items.Add(newItem);

    var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
    base.RegisterDomainEvent(newItemAddedEvent);
  }

  public void ChangeItemQuantity(int productId, uint quantity)
  {
    var item = Items.FirstOrDefault(i => i.ProductId == productId);
    Guard.Against.NotFound(productId, item, nameof(productId));
    DomainEventBase itemEvent;
    if (quantity == 0)
    {
      Items.Remove(item);
      itemEvent = new ItemDeletedEvent(this, item);
    }
    else
    {
      item.Quantity = quantity;
      itemEvent = new ChangedItemQuantityEvent(this, item);
    }

    base.RegisterDomainEvent(itemEvent);
  }
}
