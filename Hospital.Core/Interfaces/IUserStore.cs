using Hospital.Core.Models.Entities;

namespace Hospital.Core.Interfaces;

public interface IUserStore
{

    bool IsAdmin { get; }

    void SetUser(User user);

}