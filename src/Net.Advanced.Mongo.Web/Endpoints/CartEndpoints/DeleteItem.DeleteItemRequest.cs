namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class DeleteItemRequest
{
  public const string Route = "/Carts/{CartId:int}/Items/{ItemId:int}";

  public static string BuildRoute(int cartId, int itemId) => Route
    .Replace("{CartId:int}", cartId.ToString())
    .Replace("{ItemId:int}", itemId.ToString());

  public int CartId { get; set; }

  public int ItemId { get; set; }
}
