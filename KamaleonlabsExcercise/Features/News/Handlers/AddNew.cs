using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Handlers;

public class AddNewBasicPayload
{
    public string Title { get; set; }
    public string? Body { get; set; }
    public IFormFile? File { get; set; }
};

public sealed record AddNewPayload(string test);

public interface IAddNewHandler : IUseCase<AddNewBasicPayload, New>;

[CommandHandler]
public class AddNewHandler(NewsDbContext context) : IAddNewHandler
{
    private readonly DbSet<New> _news = context.News;

    public Task<New> HandleAsync(AddNewBasicPayload input, CancellationToken token = default)
    {
        string base64string = null;
        if (input.File != null)
        {
            using var memoryStream = new MemoryStream();
            input.File.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            base64string = Convert.ToBase64String(fileBytes);
        }

        New _new = new New()
        {
            Title = input.Title,
            Body = input.Body,
            Image = base64string
        };
        _news.Add(_new);

        return Task.FromResult(_new);
    }
}