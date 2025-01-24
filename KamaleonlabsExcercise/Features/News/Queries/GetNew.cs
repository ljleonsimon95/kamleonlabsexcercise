using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Queries;

/// <summary>
/// Payload for retrieving a specific news item.
/// </summary>
/// <param name="NewId">The ID of the news item to retrieve.</param>
public sealed record GetNewPayload(int NewId);

/// <summary>
/// Interface for handling the get new use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for retrieving a news item.</remarks>
public interface IGetNewHandler : IUseCase<GetNewPayload, New>;
/// <summary>
/// Command handler for retrieving a specific news item.
/// </summary>
/// <param name="context">Database context</param>
[CommandHandler]
public class GetNewHandler(NewsDbContext context) : IGetNewHandler
{
    private readonly DbSet<New> _news = context.News;

    /// <summary>
    /// Handles the operation of retrieving a specific news item.
    /// </summary>
    /// <param name="input">Payload containing the ID of the news item to retrieve.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The news item if found.</returns>
    /// <exception cref="NoFoundNewError">Thrown if the news item is not found.</exception>
    public async Task<New> HandleAsync(GetNewPayload input, CancellationToken token = default)
    {
        var news = await _news.SingleOrDefaultAsync(x => x.Id == input.NewId, token) ?? throw new NoFoundNewError();

        return news;
    }
}