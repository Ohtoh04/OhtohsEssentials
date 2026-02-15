using MediatR;
using Microsoft.AspNetCore.Http;
using OhtohsEssentials.Core.Interfaces;
using OhtohsEssentials.Web.Infrastructure.Constants;

namespace OhtohsEssentials.Web.Infrastructure.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, IUnitOfWork unitOfWork)
    {
        var transaction = unitOfWork.BeginTransaction();

        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(InfrastructureConstants.ContextItems.DomainEventsQueue, out var value) &&
                    value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                // notify the client that even though they got a good response, the changes didn't take place
                // due to an unexpected error
            }
            finally
            {
                transaction.Dispose();
            }

        });

        await _next(context);
    }
}
