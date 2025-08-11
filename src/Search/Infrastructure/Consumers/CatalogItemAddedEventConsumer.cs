using Catalog.Infrastructure.IntegrationEvents;
using MassTransit;

namespace Search.Infrastructure.Consumers
{
    public class CatalogItemAddedEventConsumer : IConsumer<CatalogItemAddedEvent>
    {
        public Task Consume(ConsumeContext<CatalogItemAddedEvent> context)
        {
      

            return Task.CompletedTask;
        }
    }
}



