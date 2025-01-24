using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.News.Errors;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Handlers;

public sealed record DeleteNewPayload(int Id);

public interface IDeleteNewHandler : IUseCase<DeleteNewPayload, New>;

[CommandHandler]
public class DeleteNewHandler(NewsDbContext context) : IDeleteNewHandler
{
    private readonly DbSet<New> _news = context.News;

    public async Task<New> HandleAsync(DeleteNewPayload input, CancellationToken token = default)
    {
        var _new = await _news.SingleOrDefaultAsync(x => x.Id == input.Id, token) ?? throw new NoFoundNewError();

        _news.Remove(_new);

        return _new;
    }
}