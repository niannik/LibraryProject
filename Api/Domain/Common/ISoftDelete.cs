namespace Domain.Common;

public interface ISoftDelete
{
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}