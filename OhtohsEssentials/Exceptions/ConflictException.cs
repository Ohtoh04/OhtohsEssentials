using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class ConflictException : ApiException
{
    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict, "RESOURCE_CONFLICT") { }
}
