using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Data;
using KamaleonlabsExcercise.Features.News.Errors;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace KamaleonlabsExcercise.Features.News.Handlers;

public sealed record EditNewPayload(string Title, string? Body);

public sealed record EditNewRecord(EditNewPayload Payload, int Id);

public interface IEditNewHandler : IUseCase<EditNewRecord, New>;

[CommandHandler]
public class EditNewHandler(NewsDbContext context) : IEditNewHandler
{
    private readonly DbSet<New> _news = context.News;

    public async Task<New> HandleAsync(EditNewRecord input, CancellationToken token = default)
    {
        var _new = await _news.SingleOrDefaultAsync(x => x.Id == input.Id, token) ?? throw new NoFoundNewError();

        _new.Title = input.Payload.Title;
        _new.Body = input.Payload.Body;
        return _new;
    }
}