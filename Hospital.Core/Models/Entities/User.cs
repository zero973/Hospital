using Ardalis.GuardClauses;
using Hospital.Core.Enums;
using Hospital.Core.Interfaces;

namespace Hospital.Core.Models.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : BaseEntity, IAggregateRoot
{
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; private set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string PasswordHash { get; private set; }
    
    /// <summary>
    /// Роль
    /// </summary>
    public UserRoles Role { get; private set; }
    
#pragma warning disable CS8618 // Required by Entity Framework
    // ReSharper disable once UnusedMember.Local
    private User() {}

    public User(string login, string passwordHash, UserRoles role)
    {
        ValidateFields(login, passwordHash);

        Login = login;
        PasswordHash = passwordHash;
        Role = role;
    }
    
    public void Edit(string login, string passwordHash, UserRoles role)
    {
        ValidateFields(login, passwordHash);

        Login = login;
        PasswordHash = passwordHash;
        Role = role;
    }

    private void ValidateFields(string login, string passwordHash)
    {
        Guard.Against.NullOrEmpty(login);
        Guard.Against.NullOrEmpty(passwordHash);
    }

}