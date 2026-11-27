using Microsoft.EntityFrameworkCore;
using System.Windows;
using curs.Data;
using curs.Data.Models;

namespace curs.Services;

/// <summary>
/// объединяет основные действия с визиткой
/// </summary>
public sealed class CardEditorFacade
{
    private readonly ExportService exportService = new();

    // сохраняет визитку в базе
    public async Task SaveAsync(BusinessCard card)
    {
        var options = new DbContextOptionsBuilder<BusinessCardDbContext>().Options;
        using var context = new BusinessCardDbContext(options);
        var service = new CardService(context);

        await service.SaveAsync(card);
    }

    // загружает визитку из базы
    public async Task<BusinessCard?> LoadAsync(int id)
    {
        var options = new DbContextOptionsBuilder<BusinessCardDbContext>().Options;
        using var context = new BusinessCardDbContext(options);
        var service = new CardService(context);

        return await service.LoadAsync(id);
    }

    // загружает все визитки из базы
    public async Task<List<BusinessCard>> GetAllAsync()
    {
        var options = new DbContextOptionsBuilder<BusinessCardDbContext>().Options;
        using var context = new BusinessCardDbContext(options);
        var service = new CardService(context);

        return await service.GetAllAsync();
    }

    // экспортирует элемент в png
    public void ExportPng(FrameworkElement element, string filePath)
    {
        exportService.ExportPng(element, filePath);
    }
}