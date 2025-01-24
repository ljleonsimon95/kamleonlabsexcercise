using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Queries;

/// <summary>
/// Payload for retrieving an image for a news item.
/// </summary>
/// <param name="NewId">The ID of the news item.</param>
public sealed record GetImageNewPayload(int NewId);

/// <summary>
/// Interface for handling the get new image use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for retrieving the image associated with a news item.</remarks>
public interface IGetNewImageHandler : IUseCase<GetImageNewPayload, string>
{
}

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
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