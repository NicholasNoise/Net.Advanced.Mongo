namespace Net.Advanced.Mongo.Infrastructure.Data;

public class MongoDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;

  public string DatabaseName { get; set; } = null!;

  public string CollectionName { get; set; } = null!;
}
