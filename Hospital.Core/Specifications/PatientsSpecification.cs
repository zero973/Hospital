using Ardalis.Specification;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Specifications;

internal class PatientsSpecification : Specification<Patient>
{

    public PatientsSpecification(Guid[] ids)
    {
        Query.Where(x => ids.Contains(x.Id));
    }

    public PatientsSpecification(string snils)
    {
        Query.Where(x => x.Snils == snils);
    }

}