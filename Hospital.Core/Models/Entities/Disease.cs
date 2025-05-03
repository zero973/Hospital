using Ardalis.GuardClauses;
using Hospital.Core.Enums;
using Hospital.Core.Interfaces;

namespace Hospital.Core.Models.Entities;

/// <summary>
/// Заболевание
/// </summary>
public class Disease : BaseEntity, IAggregateRoot
{
    
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Время года
    /// </summary>
    public PeriodTypes PeriodType { get; private set; }
    
#pragma warning disable CS8618 // Required by Entity Framework
    // ReSharper disable once UnusedMember.Local
    private Disease() {}

    public Disease(string name, PeriodTypes periodType)
    {
        Guard.Against.NullOrEmpty(name);

        Name = name;
        PeriodType = periodType;
    }

    public void Edit(string name, PeriodTypes periodType)
    {
        Guard.Against.NullOrEmpty(name);

        Name = name;
        PeriodType = periodType;
    }

    public override string ToString()
        => Name;

}