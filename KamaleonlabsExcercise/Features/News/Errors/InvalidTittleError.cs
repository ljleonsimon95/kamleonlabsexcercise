using KamaleonlabsExercise.Features.Shared;

namespace KamaleonlabsExercise.Features.News.Errors;

/// <summary>
/// Thrown when the title of a news item is empty or null.
/// </summary>
public class InvalidTittleError : ApplicationError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTittleError"/> class.
    /// </summary>
    public InvalidTittleError() : base("Name can not be empty or null.")
    {
    }
}