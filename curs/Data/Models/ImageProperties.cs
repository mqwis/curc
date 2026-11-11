namespace curs.Data.Models;

public sealed class ImageProperties
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string Stretch { get; set; } = "Uniform";
    public int ElementId { get; set; }
    public CardElement CardElement { get; set; } = null!;
}