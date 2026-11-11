namespace curs.Data.Models;

public sealed class Contact
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Position { get; set; }
    public string? Organization { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? PhotoPath { get; set; }
    public TextProperties TextProperties { get; set; } = null!;
}