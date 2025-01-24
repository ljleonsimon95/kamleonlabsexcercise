using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Handlers;

/// <summary>
/// Payload for adding a new news item.
/// </summary>
/// <param name="Title">The title of the new news item.</param>
/// <param name="Body">The body of the new news item.</param>
public sealed record AddNewPayload(string Title, string? Body);

/// <summary>
/// An interface for handling the add new use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for adding a new news item.</remarks>
public interface IAddNewHandler : IUseCase<AddNewPayload, New>
{
}

/// <summary>
/// A handler for adding a new news item.
/// </summary>
/// <remarks>This implementation encapsulates the logic for adding a new news item.</remarks>
[CommandHandler]
public class AddNewHandler(NewsDbContext context) : IAddNewHandler
{
    private readonly DbSet<New> _news = context.News;

    /// <inheritdoc/>
    public async Task<New> HandleAsync(AddNewPayload input, CancellationToken token = default)
    {
        if (input.Title is null) throw new InvalidTittleError();

        New _new = new New()
        {
            Title = input.Title,
            Body = input.Body,
        };
        await _news.AddAsync(_new, token);

        return _new;
    }
}