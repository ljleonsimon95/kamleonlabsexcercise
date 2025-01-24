using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Queries;

/// <summary>
/// Payload for retrieving all news items.
/// </summary>
public sealed record GetNewsPayload;

/// <summary>
/// Interface for handling the get all news use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for retrieving all news items.</remarks>
public interface IGetNewsHandler : IUseCase<GetNewsPayload, ICollection<New>>
{
}

/// <summary>
/// Handles the operation of retrieving all news items.
/// </summary>
/// <param name="input">Payload for retrieving all news items.</param>
/// <param name="token">Cancellation token.</param>
/// <returns>The list of news items if successful, or an empty list if the operation was cancelled.</returns>
[CommandHandler]
public class GetsNewHandler(NewsDbContext context) : IGetNewsHandler
{
    private readonly DbSet<New> _news = context.News;

    /// <inheritdoc/>
    public async Task<ICollection<New>> HandleAsync(GetNewsPayload input, CancellationToken token = default)
    {
        var news = await _news.ToListAsync(token);

        return news;
    }
}