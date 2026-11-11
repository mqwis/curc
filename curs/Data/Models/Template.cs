namespace curs.Data.Models;

public sealed class Template
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsBuiltIn { get; set; }
    public double WidthMm { get; set; }
    public double HeightMm { get; set; }
    public string BackgroundColor { get; set; } = "#FFFFFF";
    public string? PreviewPath { get; set; }
    public ICollection<BusinessCard> BusinessCards { get; set; } = new List<BusinessCard>();
    public ICollection<CardElement> Elements { get; set; } = new List<CardElement>();
}