using Microsoft.Extensions.Options;

namespace OhtohsEssentials.Options;

/// <summary>
/// Validates options.
/// </summary>
/// <typeparam name="TOptions">The options type to validate.</typeparam>
public abstract class OptionsValidator<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    protected abstract IList<string> Failures { get; init; }

    public abstract ValidateOptionsResult Validate(string? name, TOptions options);

    protected virtual void Require(string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            this.Failures.Add($"{key} is required.");
    }
}
