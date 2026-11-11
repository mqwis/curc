using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curs.Data;

public sealed class BusinessCardDbContextFactory : IDesignTimeDbContextFactory<BusinessCardDbContext>
{
    public BusinessCardDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<BusinessCardDbContext>().Options;
        return new BusinessCardDbContext(options);
    }
}