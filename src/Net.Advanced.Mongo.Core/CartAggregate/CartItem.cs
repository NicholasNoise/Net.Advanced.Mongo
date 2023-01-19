namespace Net.Advanced.Mongo.Core.CartAggregate;

public class CartItem
{
  public CartItem(int productId, string name, string? image, decimal price, uint quantity)
  {
    ProductId = productId;
    Name = name;
    Image = image;
    Price = price;
    Quantity = quantity;
  }

  public int ProductId { get; set; }

  public string Name { get; set; }

  public string? Image { get; set; }

  public decimal Price { get; set; }

  public uint Quantity { get; set; }
}
