using Ardalis.GuardClauses;
using Net.Advanced.Mongo.Core.CartAggregate.Events;
using Net.Advanced.Mongo.SharedKernel;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Core.CartAggregate;
public class Cart : EntityBase, IAggregateRoot
{
  private readonly List<CartItem> _items = new();

  public string? Name { get; set; }

  public IEnumerable<CartItem> Items => _items.AsReadOnly();

  public void AddItem(CartItem newItem)
  {
    Guard.Against.Null(newItem, nameof(newItem));
    _items.Add(newItem);

    var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
    base.RegisterDomainEvent(newItemAddedEvent);
  }
}
