namespace KamaleonlabsExercise.Features.Shared;

/// <summary>
/// An interface for describing an application use case.
/// </summary>
/// <remarks>It should encapsulate the logic required to handle
/// a use case as described by the actions that a user can perform
/// in the application</remarks>
/// <typeparam name="TInput">The type of the input received by the handler</typeparam>
/// <typeparam name="TOut">The type of the output returned by the handler</typeparam>
public interface IUseCase<TInput, TOut>
{
    /// <summary>
    /// Asynchronously handle this use case
    /// based on the provided input data.
    /// </summary>
    /// <param name="input">The input data to be processed</param>
    /// <param name="token">A cancellation token to allow dropping the processing of
    /// this request</param>
    /// <returns>A task that can be awaited for
    /// the result of this operation</returns>
    public Task<TOut> HandleAsync(TInput input, CancellationToken token = default);
}

/// <summary>
/// An interface for describing an application use case that does not require inputs
/// to be triggered.
/// </summary>
/// <remarks>Most likely used to describe readonly operations or those that toggle
/// a value in the application</remarks>
/// <typeparam name="TOut">The type of the output returned by the handler</typeparam>
public interface IUseCase<TOut>
{
    /// <summary>
    /// Asynchronously handle this use case.
    /// </summary>
    /// <param name="token">A cancellation token to allow dropping the processing of
    /// this request</param>
    /// <returns>A task that can be awaited for
    /// the result of this operation</returns>
    public Task<TOut> HandleAsync(CancellationToken token = default);
}