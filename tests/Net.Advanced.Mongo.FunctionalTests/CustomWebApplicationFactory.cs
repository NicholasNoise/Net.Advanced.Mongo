using Net.Advanced.Mongo.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Advanced.Mongo.Core.CartAggregate;
using MongoDatabaseSettings = Net.Advanced.Mongo.Infrastructure.Data.MongoDatabaseSettings;

namespace Net.Advanced.Mongo.FunctionalTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
  /// <summary>
  /// Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
  /// https://github.com/dotnet-architecture/eShopOnWeb/issues/465
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseEnvironment("Development"); // will not send real emails
    var host = builder.Build();
    host.Start();

    return host;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder
        .ConfigureServices(services =>
        {
          var settings = new MongoDatabaseSettings
          {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "CartStore",
            CollectionName = Guid.NewGuid().ToString(),
          };
          services.AddSingleton(settings);
          services.AddMongo<Cart>();
        });
  }
}
