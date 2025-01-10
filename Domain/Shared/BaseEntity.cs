namespace Domain.Shared;

public class BaseEntity : ISoftDelete
{
    public Guid Id { get; protected set; }
    public DateTime InsertDate { get; protected set; }
    public DateTime UpdateDate { get; protected set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
