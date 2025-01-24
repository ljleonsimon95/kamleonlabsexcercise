namespace KamaleonlabsExcercise.Features.News.Data;

/// <summary>
/// News entity
/// </summary>
public class New
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the news
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Body of the news
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Image associated with the news
    /// </summary>
    public string? Image { get; set; }
}