using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.News.Errors;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Queries;

public sealed record GetNewsPayload();

public interface IGetNewsHandler : IUseCase<GetNewsPayload, ICollection<New>>;

[CommandHandler]
public class GetsNewHandler(NewsDbContext context) : IGetNewsHandler
{
    private readonly DbSet<New> _news = context.News;

    public async Task<ICollection<New>> HandleAsync(GetNewsPayload input, CancellationToken token = default)
    {
        var news = await _news.ToListAsync(token);

        return news;
    }
}