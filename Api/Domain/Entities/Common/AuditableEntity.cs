namespace Domain.Entities.Common;

public abstract class Entity
{
    public int Id { get; set; }
}
public abstract class AuditableEntity : Entity , IAuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public int? LastUpdatedBy { get; set; }
}
public interface IAuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public int? LastUpdatedBy { get; set; }
}
