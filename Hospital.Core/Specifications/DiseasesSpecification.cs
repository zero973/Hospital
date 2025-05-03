using Ardalis.Specification;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Specifications;

internal class DiseasesSpecification : Specification<Disease>
{
    public DiseasesSpecification(Guid[] ids)
    {
        Query.Where(x => ids.Contains(x.Id));
    }
}