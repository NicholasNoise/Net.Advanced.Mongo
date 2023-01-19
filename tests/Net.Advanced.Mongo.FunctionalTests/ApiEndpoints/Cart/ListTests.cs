using Ardalis.HttpClientTestExtensions;
using Net.Advanced.Mongo.Web;
using Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;
using Xunit;

namespace Net.Advanced.Mongo.FunctionalTests.ApiEndpoints.Cart;

[Collection("Sequential")]
public class ListTests : BaseWebFixture
{
  public ListTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task ReturnsAllCarts()
  {
    // Act.
    var result = await Client.GetAndDeserializeAsync<CartListResponse>(List.Route);

    // Assert.
    Assert.Equal(2, result.Carts.Count);
    Assert.Contains(result.Carts, i => i.Id == SeedData.Cart1.Id);
    Assert.Contains(result.Carts, i => i.Id == SeedData.Cart2.Id);
  }
}
