using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class ImATeapotException : ApiException
{
    public ImATeapotException(string message)
        : base(message, (HttpStatusCode)418, "IM_A_TEAPOT") { }
}
