using Microsoft.Extensions.Options;

namespace OhtohsEssentials.Options;

/// <summary>
/// Validates options.
/// </summary>
/// <typeparam name="TOptions">The options type to validate.</typeparam>
public abstract class OptionsValidator<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        var failures = new List<string>();
        Validate(options, failures);

        return failures.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(failures);
    }

    protected abstract void Validate(TOptions options, ICollection<string> failures);

    protected static void Require(ICollection<string> failures, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            failures.Add($"{key} is required.");
    }
}
