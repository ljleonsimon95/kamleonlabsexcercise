using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.News.Handlers;
using KamaleonlabsExcercise.Features.News.Queries;
using KamaleonlabsExcercise.Features.News.Responses;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KamaleonlabsExcercise.Features.News.Controllers;

[ApiController]
[Route("news")]
/// <summary>
/// News controller
/// </summary>
public class NewsController(ILogger<NewsController> logger) : ControllerBase
{
    private readonly ILogger<NewsController> _logger = logger;

    /// <summary>
    /// Add a new news
    /// </summary>
    /// <param name="addNewHandler">Handler to add a new news</param>
    /// <param name="context">Database context</param>
    /// <param name="payload">Payload with new's title and body</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Ok with the id of the added new, NoContent if the operation was cancelled, or Problem if an exception occurs</returns>
    [HttpPost]
    public async Task<IActionResult> AddNew(
        [FromServices] IAddNewHandler addNewHandler,
        [FromServices] NewsDbContext context,
        [FromBody] AddNewPayload payload,
        CancellationToken token
    )
    {
        try
        {
            var result = await addNewHandler.HandleAsync(payload, token);

            await context.SaveChangesAsync(token);

            return Ok(result.Id);
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
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

    /// <summary>
    /// Edit an existing news item
    /// </summary>
    /// <param name="editNew">Handler to edit the news item</param>
    /// <param name="context">Database context</param>
    /// <param name="payload">Payload with updated new's title and body</param>
    /// <param name="id">The ID of the news item</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Ok with the id of the edited new, NoContent if the operation was cancelled, or Problem if an exception occurs</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditNew(
        [FromServices] IEditNewHandler editNew,
        [FromServices] NewsDbContext context,
        [FromBody] EditNewPayload payload,
        [FromRoute] int id,
        CancellationToken token
    )
    {
        try
        {
            var editRecord = new EditNewRecord(payload, id);
            var result = await editNew.HandleAsync(editRecord, token);

            await context.SaveChangesAsync(token);

            return Ok(result.Id);
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while editing a new.");
        }

        return Problem();
    }

    /// <summary>
    /// Delete a new news
    /// </summary>
    /// <param name="deleteNew">Handler to delete a new news</param>
    /// <param name="context">Database context</param>
    /// <param name="id">The ID of the news item to delete</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Ok with the id of the deleted new, NoContent if the operation was cancelled, or Problem if an exception occurs</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNew(
        [FromServices] IDeleteNewHandler deleteNew,
        [FromServices] NewsDbContext context,
        [FromRoute] int id,
        CancellationToken token
    )
    {
        try
        {
            var result = await deleteNew.HandleAsync(new(id), token);

            await context.SaveChangesAsync(token);

            return Ok(result.Id);
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while deleting a new.");
        }

        return Problem();
    }

    /// <summary>
    /// Add a new news
    /// </summary>
    /// <param name="addNewHandler">Handler to add a new news</param>
    /// <param name="context">Database context</param>
    /// <param name="id"></param>
    /// <param name="token">Cancellation token</param>
    /// <param name="image"></param>
    /// <returns>Ok with the id of the added new, NoContent if the operation was cancelled, or Problem if an exception occurs</returns>
    [HttpPut("image/{id:int}")]
    public async Task<IActionResult> AddOrUpdateNewImage(
        [FromServices] IAddNewImageHandler addNewHandler,
        [FromServices] NewsDbContext context,
        [FromForm] AddNewImagePayload image,
        [FromRoute] int id,
        CancellationToken token
    )
    {
        try
        {
            var result = await addNewHandler.HandleAsync(new(id, image), token);

            await context.SaveChangesAsync(token);

            return Ok(result.Id);
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while adding a new image.");
        }

        return Problem();
    }

    /// <summary>
    /// Retrieves the image associated with a news item.
    /// </summary>
    /// <param name="getNewImageHandler">Handler to get the image for a news item</param>
    /// <param name="context">Database context</param>
    /// <param name="id">The ID of the news item</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>The image as a file if successful, NoContent if the operation was cancelled, or Problem if an exception occurs</returns>
    [HttpGet("image/{id:int}")]
    public async Task<IActionResult> GetNewImage(
        [FromServices] IGetNewImageHandler getNewImageHandler,
        [FromServices] NewsDbContext context,
        [FromRoute] int id,
        CancellationToken token
    )
    {
        try
        {
            var result = await getNewImageHandler.HandleAsync(new(id), token);

            return File(Convert.FromBase64String(result), "image/jpg");
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while retrieving a new image.");
        }

        return Problem();
    }

    /// <summary>
    /// Retrieves the full data of a news item.
    /// </summary>
    /// <param name="getNewHandler">Handler to get the full data for a news item</param>
    /// <param name="context">Database context</param>
    /// <param name="id">The ID of the news item</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>The full data of the news item if successful, NoContent if the operation was cancelled, or Problem if an exception occurs</returns>
    [HttpGet("full-data/{id:int}")]
    public async Task<IActionResult> GetNewFullData(
        [FromServices] IGetNewHandler getNewHandler,
        [FromServices] NewsDbContext context,
        [FromRoute] int id,
        CancellationToken token
    )
    {
        try
        {
            var result = await getNewHandler.HandleAsync(new(id), token);

            return Ok(new SingleNewFullResponse(result));
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while retrieving full data of a news item.");
        }

        return Problem();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllNews(
        [FromServices] IGetNewsHandler getNewsHandler,
        [FromServices] NewsDbContext context,
        CancellationToken token
    )
    {
        try
        {
            var result = await getNewsHandler.HandleAsync(new(), token);

            return Ok(result.Select(newsItem => new SingleNewResponse(newsItem)).ToList());
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (ApplicationError e)
        {
            _logger.LogWarning(e, "Request failed due to app error");

            return BadRequest(e.GetProblemDetails());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception caught while retrieving full data of a news item.");
        }

        return Problem();
    }
}