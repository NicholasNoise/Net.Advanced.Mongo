using Net.Advanced.Mongo.Core.CartAggregate;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public record CartRecord(int Id, string? Name, List<CartItemRecord> Items)
{
  public static CartRecord FromCart(Cart? cart)
  {
    if (cart is null)
    {
      throw new ArgumentNullException(nameof(cart));
    }

    return new CartRecord(
      cart.Id,
      cart.Name,
      cart.Items.Select(CartItemRecord.FromCartItem).ToList());
  }
}
