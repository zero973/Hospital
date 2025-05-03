using Ardalis.Specification;

namespace Hospital.Core.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{ }