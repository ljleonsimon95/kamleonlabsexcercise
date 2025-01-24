using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Handlers;

/// <summary>
/// Payload for deleting a news item.
/// </summary>
/// <param name="Id">The ID of the news item to delete.</param>
public sealed record DeleteNewPayload(int Id);

/// <summary>
/// An interface for handling the delete new use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for deleting a new news item.</remarks>
public interface IDeleteNewHandler : IUseCase<DeleteNewPayload, New>
{
}

/// <summary>
/// A handler for deleting a new news item.
/// </summary>
/// <remarks>This implementation encapsulates the logic for deleting a new news item.</remarks>
[CommandHandler]
public class DeleteNewHandler(NewsDbContext context) : IDeleteNewHandler
{
    private readonly DbSet<New> _news = context.News;

    /// <inheritdoc/>
    public async Task<New> HandleAsync(DeleteNewPayload input, CancellationToken token = default)
    {
        var _new = await _news.SingleOrDefaultAsync(x => x.Id == input.Id, token) ?? throw new NoFoundNewError();

        _news.Remove(_new);

        return _new;
    }
}