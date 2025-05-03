using Ardalis.Specification;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Specifications;

internal class UsersSpecification : Specification<User>
{
    public UsersSpecification(string login, string passwordHash)
    {
        Query.Where(x => x.Login == login && x.PasswordHash == passwordHash);
    }
}