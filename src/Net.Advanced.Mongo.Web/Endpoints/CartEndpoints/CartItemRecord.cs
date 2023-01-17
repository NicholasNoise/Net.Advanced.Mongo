using Net.Advanced.Mongo.Core.CartAggregate;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public record CartItemRecord(int ProductId, string Name, string? Image, decimal Price, uint Quantity)
{
  public static CartItemRecord FromCartItem(CartItem? cartItem)
  {
    if (cartItem is null)
    {
      throw new ArgumentNullException(nameof(cartItem));
    }

    return new CartItemRecord(
      cartItem.ProductId,
      cartItem.Name,
      cartItem.Image,
      cartItem.Price,
      cartItem.Quantity);
  }
}
