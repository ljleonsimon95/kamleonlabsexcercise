using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.News.Errors;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Handlers;

public class AddNewImagePayload
{
    public IFormFile? File { get; set; }
};

public sealed record AddOrUpdateNewPayload(int NewId, AddNewImagePayload Payload);

public interface IAddNewImageHandler : IUseCase<AddOrUpdateNewPayload, New>;

[CommandHandler]
public class AddNewImageHandler(NewsDbContext context) : IAddNewImageHandler
{
    private readonly DbSet<New> _news = context.News;

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