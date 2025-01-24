using KamaleonlabsExercise.Features.Shared;

namespace KamaleonlabsExercise.Features.News.Errors;

/// <summary>
/// An error found when tere is no image of a given new. 
/// </summary>
public class NoFoundImageNewError() : ApplicationError("NO found image new.")
{
}