using Ardalis.Specification;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Specifications;

internal class ContactsSpecification : Specification<Contact>
{
    /// <summary>
    /// Загрузить обращение по Id с ресурсами AsTracking
    /// </summary>
    /// <param name="contactId"></param>
    public ContactsSpecification(Guid contactId)
    {
        Query.Where(x => x.Id == contactId)
            .Include(x => x.ResourcesSpent)
            .AsTracking();
    }
}