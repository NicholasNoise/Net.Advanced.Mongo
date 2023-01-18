using System.Text;
using Net.Advanced.Mongo.Web;
using Xunit;
using Net.Advanced.Mongo.Web.Endpoints.CartEndpoints;
using Newtonsoft.Json;
using System.Net;

namespace Net.Advanced.Mongo.FunctionalTests.ApiEndpoints.Cart;

[Collection("Sequential")]
public class CreateItemTests : BaseWebFixture
{
  public CreateItemTests(CustomWebApplicationFactory<WebMarker> factory)
    : base(factory)
  {
  }

  [Fact]
  public async Task CreatesItemAndCart()
  {
    // Arrange.
    const string route = CreateCartItemRequest.Route;
    var request = new CreateCartItemRequest
    {
      Id = 100,
      Item = new CartItemRecord(1, "CPU", null, 1m, 1),
    };
    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act.
    var response = await Client.PostAsync(route, jsonContent);

    // Assert.
    response.EnsureSuccessStatusCode();
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }

  [Fact]
  public async Task CreatesItemInExistingCart()
  {
    // Arrange.
    const string route = CreateCartItemRequest.Route;
    var request = new CreateCartItemRequest
    {
      Id = SeedData.Cart1.Id,
      Item = new CartItemRecord(3, "GPU", null, 10000m, 1),
    };
    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act.
    var response = await Client.PostAsync(route, jsonContent);

    // Assert.
    response.EnsureSuccessStatusCode();
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
