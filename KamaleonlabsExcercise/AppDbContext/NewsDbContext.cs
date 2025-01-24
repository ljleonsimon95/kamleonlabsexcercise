using KamaleonlabsExercise.Features.News.Data;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.AppDbContext;

public class NewsDbContext : DbContext
{
    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {
    }

    public virtual DbSet<New> News { get; set; }
}