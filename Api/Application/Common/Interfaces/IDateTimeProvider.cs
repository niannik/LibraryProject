namespace Application.Common.Interfaces;

public interface IDateTimeProvider
{
    virtual DateTime Now => DateTime.Now;
    virtual DateTime UtcNow => DateTime.UtcNow;
    virtual DateTimeOffset NowOffset => DateTimeOffset.Now;
    virtual DateTimeOffset UtcNowOffset => DateTimeOffset.Now;
}
