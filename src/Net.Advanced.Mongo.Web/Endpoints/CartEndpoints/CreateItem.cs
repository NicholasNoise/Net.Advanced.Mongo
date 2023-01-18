using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Net.Advanced.Mongo.Core.CartAggregate;
using Net.Advanced.Mongo.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class CreateItem : EndpointBaseAsync
  .WithRequest<CreateCartItemRequest>
  .WithoutResult
{
  private readonly IRepository<Cart> _repository;

  public CreateItem(IRepository<Cart> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateCartItemRequest.Route)]
  [SwaggerOperation(
    Summary = "Add item to cart",
    Description = "Add item to cart",
    OperationId = "Carts.AddItem",
    Tags = new[] { "CartEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    CreateCartItemRequest request,
    CancellationToken cancellationToken = default)
  {
    if (request.Id is null)
    {
      return BadRequest("Id is required");
    }

    if (request.Item is null)
    {
      return BadRequest("Item is required");
    }

    var cart = await _repository.GetByIdAsync(request.Id.Value, cancellationToken)
               ?? new Cart { Id = request.Id.Value, };

    var newItem = new CartItem(
      request.Item.ProductId,
      request.Item.Name,
      request.Item.Image,
      request.Item.Price,
      request.Item.Quantity);
    cart.AddItem(newItem);
    await _repository.UpdateAsync(cart, cancellationToken);

    return Ok();
  }
}
