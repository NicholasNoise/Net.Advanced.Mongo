using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Net.Advanced.Mongo.Core.CartAggregate;
using Net.Advanced.Mongo.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class DeleteItem : EndpointBaseAsync
  .WithRequest<DeleteItemRequest>
  .WithoutResult
{
  private readonly IRepository<Cart> _repository;

  public DeleteItem(IRepository<Cart> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteItemRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a Cart",
    Description = "Deletes a Cart",
    OperationId = "Categories.Delete",
    Tags = new[] { "CartEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteItemRequest request,
    CancellationToken cancellationToken = default)
  {
    var cartToUpdate = await _repository.GetByIdAsync(request.CartId, cancellationToken);
    if (cartToUpdate is null)
    {
      return NotFound(nameof(Cart));
    }

    cartToUpdate.ChangeItemQuantity(request.ItemId, 0);

    await _repository.UpdateAsync(cartToUpdate, cancellationToken);

    return Ok();
  }
}
