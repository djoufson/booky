using Microsoft.EntityFrameworkCore;

namespace Shared.EF.Database;

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
