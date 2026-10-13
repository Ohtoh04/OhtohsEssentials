namespace OhtohsEssentials.Web.Responses;

public record ProfilingData(
    int ElapsedTimeInMilliseconds,
    DateTimeOffset requestStartUtc,
    DateTimeOffset requestEndUtc);
