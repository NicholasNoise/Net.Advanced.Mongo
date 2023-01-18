using FastEndpoints;
using Net.Advanced.Mongo.Core.CartAggregate;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;

public class List : EndpointWithoutRequest<CartListResponse>
{
  private readonly IRepository<Cart> _repository;

  public const string Route = "/Carts";

  public List(IRepository<Cart> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("CartEndpoints"));
  }
  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var carts = await _repository.ListAsync(cancellationToken);
    var response = new CartListResponse
    {
      Carts = carts
        .Select(CartRecord.FromCart)
        .ToList()
    };

    await SendAsync(response, cancellation: cancellationToken);
  }
}
