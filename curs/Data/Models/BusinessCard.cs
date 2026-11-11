namespace curs.Data.Models;

public sealed class BusinessCard
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double WidthMm { get; set; }
    public double HeightMm { get; set; }
    public string BackgroundColor { get; set; } = "#FFFFFF";
    public int? TemplateId { get; set; }
    public Template? Template { get; set; }
    public ICollection<CardElement> Elements { get; set; } = new List<CardElement>();
}