using Ardalis.GuardClauses;
using Hospital.Core.Interfaces;

namespace Hospital.Core.Models.Entities;

/// <summary>
/// Обращение пациента в поликлинику
/// </summary>
public class Contact : BaseEntity, IAggregateRoot
{
    
    /// <summary>
    /// Обратившийся пациент
    /// </summary>
    public Guid PatientId { get; private set; }
    
    /// <summary>
    /// Заболевание
    /// </summary>
    public Guid DiseaseId { get; private set; }
    
    /// <summary>
    /// Дата обращения
    /// </summary>
    public DateOnly Date { get; private set; }
    
    /// <summary>
    /// Затраченные ресурсы
    /// </summary>
    private readonly List<ResourceSpent> _resourcesSpent = new();

    /// <summary>
    /// Затраченные ресурсы
    /// </summary>
    public IReadOnlyCollection<ResourceSpent> ResourcesSpent => _resourcesSpent.AsReadOnly();
      
#pragma warning disable CS8618 // Required by Entity Framework
    // ReSharper disable once UnusedMember.Local
    private Contact() {}

    public Contact(Guid patientId, Guid diseaseId, DateOnly date, List<ResourceSpent> resourcesSpent)
    {
        Guard.Against.Null(resourcesSpent);

        PatientId = patientId;
        DiseaseId = diseaseId;
        Date = date;
        _resourcesSpent = resourcesSpent;
    }

    public void Edit(Guid patientId, Guid diseaseId, DateOnly date)
    {
        PatientId = patientId;
        DiseaseId = diseaseId;
        Date = date;
    }
    
    public bool AddResourceSpent(ResourceSpent resourceSpent)
    {
        if (_resourcesSpent.Contains(resourceSpent))
            return false;

        _resourcesSpent.Add(resourceSpent);
        
        return true;
    }

    public bool RemoveResourceSpent(ResourceSpent resourceSpent) 
        => _resourcesSpent.Remove(resourceSpent);
    
}