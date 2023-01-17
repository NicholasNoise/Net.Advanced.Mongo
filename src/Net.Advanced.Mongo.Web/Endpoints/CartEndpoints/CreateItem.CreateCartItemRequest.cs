using System.ComponentModel.DataAnnotations;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class CreateCartItemRequest
{
  public const string Route = "/Carts";

  [Required]
  public int? Id { get; set; }

  [Required]
  public CartItemRecord? Item { get; set; }
}
