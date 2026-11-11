namespace curs.Data.Models;

public sealed class TextProperties
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string FontFamily { get; set; } = "Arial";
    public double FontSize { get; set; } = 12;
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
    public bool IsUnderline { get; set; }
    public string Foreground { get; set; } = "#000000";
    public string TextAlignment { get; set; } = "Left";
    public int ElementId { get; set; }
    public CardElement CardElement { get; set; } = null!;
    public Contact? Contact { get; set; }
}