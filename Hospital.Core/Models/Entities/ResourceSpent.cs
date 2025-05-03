using Ardalis.GuardClauses;

namespace Hospital.Core.Models.Entities;

/// <summary>
/// Затраченный ресурс при обращении пациента
/// </summary>
#pragma warning disable CS0659
public class ResourceSpent
{
    
    /// <summary>
    /// Название затраченного ресурса
    /// </summary>
    public string Resource { get; private set; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment { get; private set; }
    
    /// <summary>
    /// Кол-во затраченного ресурса
    /// </summary>
    public uint Count { get; private set; }
    
#pragma warning disable CS8618 // Required by Entity Framework
    // ReSharper disable once UnusedMember.Local
    private ResourceSpent() {}

    public ResourceSpent(string resource, string comment, uint count)
    {
        ValidateFields(resource, comment);

        Resource = resource;
        Comment = comment;
        Count = count;
    }
    
    public void Edit(Guid contactId, string resource, string comment, uint count)
    {
        ValidateFields(resource, comment);

        Resource = resource;
        Comment = comment;
        Count = count;
    }

#pragma warning disable CS0659
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not ResourceSpent other) return false;

        return Resource.ToLower() == other.Resource.ToLower();
    }

    private void ValidateFields(string resource, string comment)
    {
        Guard.Against.NullOrEmpty(resource);
        Guard.Against.NullOrEmpty(comment);
    }

}