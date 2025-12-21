namespace DevTrivia.API.Capabilities.Trivia.Models;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime Ins { get; set; } = DateTime.UtcNow;
    public DateTime? Upd { get; set; }
}