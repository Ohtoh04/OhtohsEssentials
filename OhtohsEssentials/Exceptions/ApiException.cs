using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string ErrorCode { get; }

    public ApiException(string message, HttpStatusCode statusCode, string errorCode)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
