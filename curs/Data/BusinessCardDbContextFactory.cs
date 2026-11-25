using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curs.Data;

/// <summary>
/// создаёт контекст базы для миграций
/// </summary>
public sealed class BusinessCardDbContextFactory : IDesignTimeDbContextFactory<BusinessCardDbContext>
{
    // создаёт контекст для ef core
    public BusinessCardDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<BusinessCardDbContext>().Options;
        return new BusinessCardDbContext(options);
    }
}