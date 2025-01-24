using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Handlers;

/// <summary>
/// Payload for editing a news item.
/// </summary>
/// <param name="Title">The new title of the news item.</param>
/// <param name="Body">The new body of the news item.</param>
public sealed record EditNewPayload(string Title, string? Body);

/// <summary>
/// Record containing the payload and ID for editing a news item.
/// </summary>
/// <param name="Payload">The payload with the new title and body.</param>
/// <param name="Id">The ID of the news item to edit.</param>
public sealed record EditNewRecord(EditNewPayload Payload, int Id);

/// <summary>
/// Interface for handling the edit new use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for editing a news item.</remarks>
public interface IEditNewHandler : IUseCase<EditNewRecord, New>
{
}

/// <summary>
/// Command handler for editing a news item.
/// </summary>
/// <param name="context">Database context</param>
[CommandHandler]
public class EditNewHandler(NewsDbContext context) : IEditNewHandler
{
    private readonly DbSet<New> _news = context.News;

    /// <summary>
    /// Handles the operation of editing a news item.
    /// </summary>
    /// <param name="input">Record containing the payload and ID for editing.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The edited news item.</returns>
    /// <exception cref="NoFoundNewError">Thrown if the news item is not found.</exception>
    public async Task<New> HandleAsync(EditNewRecord input, CancellationToken token = default)
    {
        var _new = await _news.SingleOrDefaultAsync(x => x.Id == input.Id, token) ?? throw new NoFoundNewError();

        _new.Title = input.Payload.Title;
        _new.Body = input.Payload.Body;
        return _new;
    }
}