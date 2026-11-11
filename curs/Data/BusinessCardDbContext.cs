using Microsoft.EntityFrameworkCore;
using curs.Data.Models;

namespace curs.Data;

public sealed class BusinessCardDbContext : DbContext
{
    private const string DefaultConnectionString =
        "Host=localhost;Port=5432;Database=business_cards;Username=postgres;Password=123";

    public BusinessCardDbContext(DbContextOptions<BusinessCardDbContext> options)
        : base(options)
    {
    }

    public DbSet<Template> Templates => Set<Template>();
    public DbSet<BusinessCard> BusinessCards => Set<BusinessCard>();
    public DbSet<CardElement> CardElements => Set<CardElement>();
    public DbSet<ImageProperties> ImageProperties => Set<ImageProperties>();
    public DbSet<TextProperties> TextProperties => Set<TextProperties>();
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("BUSINESS_CARDS_CONNECTION_STRING")
                ?? DefaultConnectionString;

            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Template>(entity =>
        {
            entity.ToTable("Templates");
            entity.HasKey(template => template.Id);
            entity.Property(template => template.Name).HasMaxLength(150).IsRequired();
            entity.Property(template => template.BackgroundColor).HasMaxLength(32).IsRequired();
            entity.Property(template => template.PreviewPath).HasMaxLength(500);
            entity.HasMany(template => template.BusinessCards)
                .WithOne(card => card.Template)
                .HasForeignKey(card => card.TemplateId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(template => template.Elements)
                .WithOne(element => element.Template)
                .HasForeignKey(element => element.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BusinessCard>(entity =>
        {
            entity.ToTable("BusinessCards");
            entity.HasKey(card => card.Id);
            entity.Property(card => card.Name).HasMaxLength(150).IsRequired();
            entity.Property(card => card.BackgroundColor).HasMaxLength(32).IsRequired();
            entity.HasMany(card => card.Elements)
                .WithOne(element => element.BusinessCard)
                .HasForeignKey(element => element.BusinessCardId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CardElement>(entity =>
        {
            entity.ToTable("CardElements", table => table.HasCheckConstraint(
                "CK_CardElements_ExactlyOneOwner",
                "(\"BusinessCardId\" IS NULL) <> (\"TemplateId\" IS NULL)"));
            entity.HasKey(element => element.Id);
            entity.Property(element => element.ElementType).HasConversion<string>().HasMaxLength(16);
            entity.Property(element => element.Name).HasMaxLength(150).IsRequired();
            entity.Property(element => element.StrokeColor).HasMaxLength(32);
            entity.Property(element => element.FillColor).HasMaxLength(32);
            entity.HasOne(element => element.TextProperties)
                .WithOne(properties => properties.CardElement)
                .HasForeignKey<TextProperties>(properties => properties.ElementId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(element => element.ImageProperties)
                .WithOne(properties => properties.CardElement)
                .HasForeignKey<ImageProperties>(properties => properties.ElementId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ImageProperties>(entity =>
        {
            entity.ToTable("ImageProperties");
            entity.HasKey(properties => properties.Id);
            entity.Property(properties => properties.ImagePath).HasMaxLength(500).IsRequired();
            entity.Property(properties => properties.Stretch).HasMaxLength(32).IsRequired();
        });

        modelBuilder.Entity<TextProperties>(entity =>
        {
            entity.ToTable("TextProperties");
            entity.HasKey(properties => properties.Id);
            entity.Property(properties => properties.Content).IsRequired();
            entity.Property(properties => properties.FontFamily).HasMaxLength(100).IsRequired();
            entity.Property(properties => properties.Foreground).HasMaxLength(32).IsRequired();
            entity.Property(properties => properties.TextAlignment).HasMaxLength(16).IsRequired();
            entity.HasOne(properties => properties.Contact)
                .WithOne(contact => contact.TextProperties)
                .HasForeignKey<Contact>(contact => contact.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts");
            entity.HasKey(contact => contact.Id);
            entity.Property(contact => contact.FullName).HasMaxLength(200).IsRequired();
            entity.Property(contact => contact.Position).HasMaxLength(150);
            entity.Property(contact => contact.Organization).HasMaxLength(200);
            entity.Property(contact => contact.Phone).HasMaxLength(50);
            entity.Property(contact => contact.Email).HasMaxLength(254);
            entity.Property(contact => contact.Website).HasMaxLength(500);
            entity.Property(contact => contact.Address).HasMaxLength(500);
            entity.Property(contact => contact.PhotoPath).HasMaxLength(500);
        });
    }
}