using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized, "UNAUTHORIZED") { }
}
