using Ardalis.GuardClauses;
using Hospital.Core.Interfaces;

namespace Hospital.Core.Models.Entities;

/// <summary>
/// Пациент
/// </summary>
public class Patient : BaseEntity, IAggregateRoot
{
    
    /// <summary>
    /// ФИО
    /// </summary>
    public string FIO { get; private set; }
    
    /// <summary>
    /// День рождения
    /// </summary>
    public DateOnly Birthday { get; private set; }
    
    /// <summary>
    /// СНИЛС
    /// </summary>
    public string Snils { get; private set; }
    
#pragma warning disable CS8618 // Required by Entity Framework
    // ReSharper disable once UnusedMember.Local
    private Patient() {}

    public Patient(string fio, DateOnly birthday, string snils)
    {
        ValidateFields(fio, birthday, snils);

        FIO = fio;
        Birthday = birthday;
        Snils = snils;
    }
    
    public void Edit(string fio, DateOnly birthday, string snils)
    {
        ValidateFields(fio, birthday, snils);

        FIO = fio;
        Birthday = birthday;
        Snils = snils;
    }

    private void ValidateFields(string fio, DateOnly birthday, string snils)
    {
        Guard.Against.NullOrEmpty(fio);
        Guard.Against.Expression((DateOnly date) => date >= DateOnly.FromDateTime(DateTime.Now),
            birthday, "День рождения должен быть меньше текущей даты");
        Guard.Against.LengthOutOfRange(snils, 11, 11, message: "СНИЛС должен содержать 11 цифр");
    }

    public override string ToString() 
        => FIO;

}