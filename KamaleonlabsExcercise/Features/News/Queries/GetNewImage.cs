using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.News.Errors;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Queries;

public sealed record GetImageNewPayload(int NewId);

public interface IGetNewImageHandler : IUseCase<GetImageNewPayload, string>;

[CommandHandler]
public class GetNewImageHandler(NewsDbContext context) : IGetNewImageHandler
{
    private readonly DbSet<New> _news = context.News;

    public async Task<string> HandleAsync(GetImageNewPayload input, CancellationToken token = default)
    {
        var news = await _news.SingleOrDefaultAsync(x => x.Id == input.NewId, token) ?? throw new NoFoundNewError();
        if (news.Image is null)
        {
            throw new NoFoundImageNewError();
        }

        return news.Image;
    }
}