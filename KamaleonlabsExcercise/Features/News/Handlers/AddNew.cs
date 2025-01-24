using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Handlers;

public sealed record AddNewPayload(string Title, string? Body);

public interface IAddNewHandler : IUseCase<AddNewPayload, New>;

[CommandHandler]
public class AddNewHandler(NewsDbContext context) : IAddNewHandler
{
    private readonly DbSet<New> _news = context.News;

    public Task<New> HandleAsync(AddNewPayload input, CancellationToken token = default)
    {
        New _new = new New()
        {
            Title = input.Title,
            Body = input.Body,
        };
        _news.Add(_new);

        return Task.FromResult(_new);
    }
}