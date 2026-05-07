namespace OhtohsEssentials.Pipelining;

/// <summary>
/// Represents a handler in a chain of responsibility
/// </summary>
/// <typeparam name="TContext">The context type that flows through the pipeline</typeparam>
public interface IHandler<TContext>
{
    /// <summary>
    /// Handles the context or passes to next handler
    /// </summary>
    /// <returns>True if pipeline should continue, false to stop</returns>
    Task<bool> HandleAsync(TContext context, Func<TContext, Task<bool>> next);
}

/// <summary>
/// Simple synchronous handler interface
/// </summary>
/// <typeparam name="TContext">The context type that flows through the pipeline</typeparam>
public interface IHandlerSync<TContext>
{
    bool Handle(TContext context, Func<TContext, bool> next);
}
