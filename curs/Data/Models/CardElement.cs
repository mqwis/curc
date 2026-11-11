namespace curs.Data.Models;

public sealed class CardElement
{
    public int Id { get; set; }
    public CardElementType ElementType { get; set; }
    public string Name { get; set; } = string.Empty;
    public double PositionX { get; set; }
    public double PositionY { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Rotation { get; set; }
    public double Opacity { get; set; } = 1;
    public int ZOrder { get; set; }
    public bool IsLocked { get; set; }
    public string? StrokeColor { get; set; }
    public double StrokeThickness { get; set; }
    public string? FillColor { get; set; }
    public int? BusinessCardId { get; set; }
    public BusinessCard? BusinessCard { get; set; }
    public int? TemplateId { get; set; }
    public Template? Template { get; set; }
    public TextProperties? TextProperties { get; set; }
    public ImageProperties? ImageProperties { get; set; }
}