using Hospital.Core.Models.Entities;

namespace Hospital.Core.Models.DTO;

/// <summary>
/// Обращение пациента в поликлинику
/// </summary>
public class ContactDto
{

    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Обратившийся пациент
    /// </summary>
    public Patient Patient { get; set; }
    
    /// <summary>
    /// Заболевание
    /// </summary>
    public Disease Disease { get; set; }
    
    /// <summary>
    /// Дата обращения
    /// </summary>
    public DateOnly Date { get; set; }
    
    /// <summary>
    /// Затраченные ресурсы
    /// </summary>
    public List<ResourceSpent> ResourcesSpent { get; set; }

    public ContactDto(Guid id, Patient patient, Disease disease, DateOnly date, List<ResourceSpent> resourcesSpent)
    {
        Id = id;
        Patient = patient;
        Disease = disease;
        Date = date;
        ResourcesSpent = resourcesSpent;
    }

    public override string ToString()
    {
        return $"Id: {Id}";
    }

    public bool Equals(BaseEntity other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj is BaseEntity entity && Equals(entity);
    }

    public override int GetHashCode() => Id.GetHashCode();

}