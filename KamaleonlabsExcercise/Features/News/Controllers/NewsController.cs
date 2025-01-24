using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KamaleonlabsExcercise.Features.News.Controllers;

[ApiController]
[Route("news")]
public class NewsController(ILogger<NewsController> logger) : ControllerBase
{
    private readonly ILogger<NewsController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> AddNew(
        [FromServices] IAddNewHandler addNewHandler,
        [FromServices] NewsDbContext context,
        [FromForm] AddNewBasicPayload payload,
        // [FromForm] IFormFile file,
        CancellationToken token
    )
    {
        try
        {
            var result = await addNewHandler.HandleAsync(payload, token);
            
            await context.SaveChangesAsync(token);

            return Ok(1);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while adding a new.");
        }

        return Problem();
    }
}