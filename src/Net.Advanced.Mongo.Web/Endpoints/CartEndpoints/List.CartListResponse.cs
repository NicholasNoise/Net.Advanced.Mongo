namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class CartListResponse
{
  public List<CartRecord> Carts { get; set; } = new();
}
