using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Mongo.Web;
using Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;
using Xunit;

namespace Net.Advanced.Mongo.FunctionalTests.ApiEndpoints.Cart;

[Collection("Sequential")]
public class GetByIdTests : BaseWebFixture
{
  public GetByIdTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task ReturnsAllCategories()
  {
    // Arrange.
    string route = GetCartByIdRequest.BuildRoute(SeedData.Cart1.Id);

    // Act.
    var result = await Client.GetAndDeserializeAsync<CartRecord>(route);

    // Assert.
    Assert.Equal(SeedData.Cart1.Id, result.Id);
    Assert.Equal(SeedData.Cart1.Name, result.Name);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenId0()
  {
    // Arrange.
    const int cartId = 0;
    string route = GetCartByIdRequest.BuildRoute(cartId);

    // Assert.
    _ = await Client.GetAndEnsureNotFoundAsync(route);
  }
}
