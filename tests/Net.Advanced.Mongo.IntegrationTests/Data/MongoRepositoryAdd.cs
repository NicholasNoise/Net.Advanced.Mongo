using Net.Advanced.Mongo.Core.CartAggregate;
using Xunit;

namespace Net.Advanced.Mongo.IntegrationTests.Data;

public class MongoRepositoryAdd : BaseMongoRepoTestFixture
{
  protected override string DbName => nameof(MongoRepositoryAdd);

  [Fact]
  public async Task AddsCartAndSetsId()
  {
    // Arrange.
    const string testCartName = "testCart";
    var repository = GetRepository();
    var project = new Cart { Name = testCartName };

    // Act.
    await repository.AddAsync(project);

    var newCart = (await repository.ListAsync())
                    .FirstOrDefault();

    // Assert.
    Assert.NotNull(newCart);
    Assert.Equal(testCartName, newCart.Name);
    Assert.True(newCart.Id > 0);
  }
}
