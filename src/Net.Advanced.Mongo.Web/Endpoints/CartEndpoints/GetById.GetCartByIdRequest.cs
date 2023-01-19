namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class GetCartByIdRequest
{
  public const string Route = "/Carts/{CartId:int}";
  public static string BuildRoute(int cartId) => Route.Replace("{CartId:int}", cartId.ToString());

  public int CartId { get; set; }
}
