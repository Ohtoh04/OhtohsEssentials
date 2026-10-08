using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound, "RESOURCE_NOT_FOUND") { }
}
