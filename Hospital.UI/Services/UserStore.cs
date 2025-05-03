using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;

namespace Hospital.UI.Services;

internal class UserStore : IUserStore
{

    private User? _user { get; set; }

    public bool IsAdmin => _user?.Role == Core.Enums.UserRoles.Admin;

    public void SetUser(User user)
        => _user = user;

}