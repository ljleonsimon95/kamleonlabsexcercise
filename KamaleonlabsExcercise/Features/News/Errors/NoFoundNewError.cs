using KamaleonlabsExercise.Features.Shared;

namespace KamaleonlabsExercise.Features.News.Errors;

/// <summary>
/// An error found whe fetching a no existing new.
/// </summary>
public class NoFoundNewError() : ApplicationError("NO found new.")
{
}