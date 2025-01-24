using KamaleonlabsExcercise.Features.News.Data;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.AppDbContext;

public class NewsDbContext : DbContext
{
    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {
    }

    public DbSet<New> News { get; set; }
}