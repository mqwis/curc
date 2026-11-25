using Microsoft.EntityFrameworkCore;
using curs.Data.Models;

namespace curs.Data;

// сохраняет и загружает визитки из базы
/// <summary>
/// сохраняет и загружает визитки
/// </summary>
public sealed class CardService
{
    private readonly BusinessCardDbContext context;

    // получает доступ к базе визиток
    public CardService(BusinessCardDbContext context)
    {
        this.context = context;
    }

    // сохраняет новую визитку или обновляет старую
    public async Task SaveAsync(BusinessCard card)
    {
        if (card.Id == 0)
        {
            context.BusinessCards.Add(card);
        }
        else
        {
            context.BusinessCards.Update(card);
        }

        await context.SaveChangesAsync();
    }

    // загружает одну визитку с элементами
    public async Task<BusinessCard?> LoadAsync(int id)
    {
        return await context.BusinessCards
            .Include(card => card.Elements)
            .FirstOrDefaultAsync(card => card.Id == id);
    }

    // получает все визитки для списка
    public async Task<List<BusinessCard>> GetAllAsync()
    {
        return await context.BusinessCards
            .OrderBy(card => card.Name)
            .ToListAsync();
    }
}
