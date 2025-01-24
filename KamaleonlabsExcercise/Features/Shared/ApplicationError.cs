using Microsoft.AspNetCore.Mvc;

namespace KamaleonlabsExercise.Features.Shared;

/// <summary>
/// Base class for errors thrown by the app
/// </summary>
public class ApplicationError(string message) : Exception(message)
{
    public ProblemDetails GetProblemDetails() =>
        new()
        {
            Type = "https://bxcentral.net/api/application-error",
            Detail = Message,
            Title = "Application error",
            Status = StatusCodes.Status400BadRequest,
        };  
}