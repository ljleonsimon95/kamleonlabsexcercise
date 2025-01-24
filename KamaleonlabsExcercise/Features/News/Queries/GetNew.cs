using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.News.Errors;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Queries;

public sealed record GetNewPayload(int NewId);

public interface IGetNewHandler : IUseCase<GetNewPayload, New>;

[CommandHandler]
public class GetNewHandler(NewsDbContext context) : IGetNewHandler
{
    private readonly DbSet<New> _news = context.News;

    public async Task<New> HandleAsync(GetNewPayload input, CancellationToken token = default)
    {
        var news = await _news.SingleOrDefaultAsync(x => x.Id == input.NewId, token) ?? throw new NoFoundNewError();

        return news;
    }
}