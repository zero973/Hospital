using Ardalis.Specification;

namespace Hospital.Core.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{ }