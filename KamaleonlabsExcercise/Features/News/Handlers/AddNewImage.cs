using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExercise.Features.News.Handlers;

/// <summary>
/// Payload for adding or updating a new news item's image.
/// </summary>
/// <param name="NewId">The ID of the news item.</param>
/// <param name="Payload">The image payload.</param>
public sealed record AddOrUpdateNewPayload(int NewId, AddNewImagePayload Payload);

/// <summary>
/// Payload for adding an image to a new news item.
/// </summary>
/// <param name="File">The image file.</param>
public class AddNewImagePayload
{
    /// <summary>
    /// The image file.
    /// </summary>
    public IFormFile? File { get; set; }
}

/// <summary>
/// An interface for handling the add new image use case.
/// </summary>
/// <remarks>This use case encapsulates the logic for adding a new image to a news item.</remarks>
public interface IAddNewImageHandler : IUseCase<AddOrUpdateNewPayload, New>
{
}

/// <summary>
/// Command handler for adding or updating an image for a news item.
/// </summary>
[CommandHandler]
public class AddNewImageHandler(NewsDbContext context) : IAddNewImageHandler
{
    private readonly DbSet<New> _news = context.News;

    /// <summary>
    /// Handles the operation of adding or updating a news item's image.
    /// </summary>
    /// <param name="input">Payload containing the news ID and image file.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The updated news item.</returns>
    /// <exception cref="NoFoundNewError">Thrown if the news item is not found.</exception>
    public async Task<New> HandleAsync(AddOrUpdateNewPayload input, CancellationToken token = default)
    {
        var _new = await _news.SingleOrDefaultAsync(x => x.Id == input.NewId, token) ?? throw new NoFoundNewError();

        string base64String = null;
        if (input.Payload.File != null)
        {
            using var memoryStream = new MemoryStream();
            input.Payload.File.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            base64String = Convert.ToBase64String(fileBytes);
        }

        _new.Image = base64String;

        return _new;
    }
}