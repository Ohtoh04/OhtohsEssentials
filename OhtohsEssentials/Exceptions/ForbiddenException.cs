using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class ForbiddenException : ApiException
{
    public ForbiddenException(string message)
        : base(message, HttpStatusCode.Forbidden, "FORBIDDEN") { }
}
