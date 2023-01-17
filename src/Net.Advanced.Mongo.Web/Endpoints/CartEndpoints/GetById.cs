using FastEndpoints;
using Net.Advanced.Mongo.Core.CartAggregate;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class GetById : Endpoint<GetCartByIdRequest, CartRecord>
{
  private readonly IRepository<Cart> _repository;

  public GetById(IRepository<Cart> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(GetCartByIdRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("CartEndpoints"));
  }
  public override async Task HandleAsync(
    GetCartByIdRequest request, 
    CancellationToken cancellationToken)
  {
    var entity = await _repository.GetByIdAsync(request.CartId, cancellationToken);
    if (entity == null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var response = CartRecord.FromCart(entity);

    await SendAsync(response, cancellation: cancellationToken);
  }
}
