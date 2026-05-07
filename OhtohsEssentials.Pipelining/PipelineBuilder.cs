namespace OhtohsEssentials.Pipelining;

public class PipelineBuilder<TContext>
{
    private readonly List<Func<Func<TContext, Task<bool>>, Func<TContext, Task<bool>>>> _components = new();

    /// <summary>
    /// Add a handler to the pipeline
    /// </summary>
    public PipelineBuilder<TContext> Use(IHandler<TContext> handler)
    {
        _components.Add(next => async context =>
            await handler.HandleAsync(context, next));
        return this;
    }

    /// <summary>
    /// Add a handler using a delegate
    /// </summary>
    public PipelineBuilder<TContext> Use(Func<TContext, Func<TContext, Task<bool>>, Task<bool>> handler)
    {
        _components.Add(next => async context =>
            await handler(context, next));
        return this;
    }

    /// <summary>
    /// Build the pipeline
    /// </summary>
    public Func<TContext, Task<bool>> Build()
    {
        Func<TContext, Task<bool>> pipeline = _ => Task.FromResult(true);

        for (int i = _components.Count - 1; i >= 0; i--)
        {
            pipeline = _components[i](pipeline);
        }

        return pipeline;
    }
}

// Usage example:

//public class EmailSpamDetector
//{
//    private readonly Func<SpamDetectionContext, Task<bool>> _pipeline;

//    public EmailSpamDetector()
//    {
//        _pipeline = new PipelineBuilder<SpamDetectionContext>()
//            .Use(new BlacklistCheck())
//            .Use(new SuspiciousKeywordsCheck())
//            .Use(new AttachmentCheck())
//            .Use(new SpamScoreThresholdHandler())
//            .AddCheck("ValidSenderFormat", ctx =>
//                ctx.Email.Sender.Contains("@") && !ctx.Email.Sender.StartsWith("noreply"))
//            .Build();
//    }

//    public async Task<SpamDetectionResult> DetectSpam(EmailMessage email)
//    {
//        var context = new SpamDetectionContext
//        {
//            Email = email,
//            IsSpam = false
//        };

//        await _pipeline(context);

//        return new SpamDetectionResult
//        {
//            IsSpam = context.IsSpam,
//            Reason = context.Reason,
//            SpamScore = email.SpamScore,
//            TriggeredRules = email.TriggeredRules
//        };
//    }
//}

//public class SpamDetectionResult
//{
//    public bool IsSpam { get; set; }
//    public string Reason { get; set; }
//    public double SpamScore { get; set; }
//    public List<string> TriggeredRules { get; set; }
//}

//// Usage
//class Program
//{
//    static async Task Main()
//    {
//        var detector = new EmailSpamDetector();

//        var email = new EmailMessage
//        {
//            Sender = "winner@fake-mail.org",
//            Subject = "YOU ARE A WINNER!",
//            Body = "Congratulations! You've won a lottery. Click here to claim your prize.",
//            Attachments = new List<string> { "prize_details.exe" }
//        };

//        var result = await detector.DetectSpam(email);

//        Console.WriteLine($"Is Spam: {result.IsSpam}");
//        Console.WriteLine($"Reason: {result.Reason}");
//        Console.WriteLine($"Spam Score: {result.SpamScore}");
//        Console.WriteLine("Triggered Rules:");
//        foreach (var rule in result.TriggeredRules)
//        {
//            Console.WriteLine($"  - {rule}");
//        }
//    }
//}
