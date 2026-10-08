using System.Net;

namespace OhtohsEssentials.Core.Exceptions;

public class PaymentRequiredException : ApiException
{
    public PaymentRequiredException(string message)
        : base(message, HttpStatusCode.PaymentRequired, "PAYMENT_REQUIRED") { }
}
