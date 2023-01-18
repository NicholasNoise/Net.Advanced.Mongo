using System.Net;
using Net.Advanced.Mongo.Web;
using Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;
using Xunit;

namespace Net.Advanced.Mongo.FunctionalTests.ApiEndpoints.Cart;

[Collection("Sequential")]
public class DeleteItemTests : BaseWebFixture
{
  public DeleteItemTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task DeletesSeededItem()
  {
    // Arrange.
    string route = DeleteItemRequest.BuildRoute(SeedData.Cart1.Id, SeedData.CartItem2.ProductId);

    // Act.
    var response = await Client.DeleteAsync(route);

    // Assert.
    response.EnsureSuccessStatusCode();
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
