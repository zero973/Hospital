using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Hospital.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data;

internal class EfRepository<T>(AppDbContext dbContext) 
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public override async Task<int> DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).ExecuteDeleteAsync(cancellationToken);
    }
}