using Ardalis.Specification;

namespace Net.Advanced.Mongo.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
