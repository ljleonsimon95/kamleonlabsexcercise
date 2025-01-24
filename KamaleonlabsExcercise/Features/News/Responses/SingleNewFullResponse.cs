using KamaleonlabsExcercise.Features.News.Data;

namespace KamaleonlabsExcercise.Features.News.Responses;

/// <summary>
/// A response containing the full data of a <see cref="New"/>.
/// </summary>
/// <param name="source">The <see cref="New"/> to be used as the data source.</param>
public class SingleNewResponse(New source)
{
    /// <summary>
    /// The ID of the new.
    /// </summary>
    public int Id => source.Id;

    /// <summary>
    /// The title of the new.
    /// </summary>
    public string Tittle => source.Title;

    /// <summary>
    /// The body of the new (if available).
    /// </summary>
    public string? Body => source.Body;
}

/// <summary>
/// A response containing the full data of a <see cref="New"/>, including the image.
/// </summary>
/// <param name="source">The <see cref="New"/> to be used as the data source.</param>
public class SingleNewFullResponse(New source) : SingleNewResponse(source)
{
    /// <summary>
    /// A base64-encoded string representing the image of the new.
    /// </summary>
    public string ImageUrl => $"data:image/jpg;base64,{source.Image}";
}