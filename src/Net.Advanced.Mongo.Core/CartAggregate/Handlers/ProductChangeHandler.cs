using Net.Advanced.Mongo.SharedKernel;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Core.CartAggregate.Handlers;

public class ProductChangeHandler
{
  private readonly IRepository<Cart> _repository;

  public ProductChangeHandler(IRepository<Cart> repository)
  {
    _repository = repository;
  }

  public async Task Handle(EntityChangedEvent<Product> entityChangedEvent, CancellationToken cancellationToken = default)
  {
    var product = entityChangedEvent.Entity;
    var carts = await _repository.ListAsync(cancellationToken);
    foreach (var cart in carts)
    {
      var itemToChange = cart.Items
        .Where(item => item.ProductId == product.Id)
        .ToList();
      if (itemToChange.Any())
      {
        foreach (var cartItem in itemToChange)
        {
          cartItem.Name = product.Name;
          cartItem.Image = product.Image;
          cartItem.Price = product.Price;
        }

        await _repository.UpdateAsync(cart, cancellationToken);
      }
    }
  }
}
