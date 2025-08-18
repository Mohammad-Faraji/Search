using Catalog.Infrastructure.IntegrationEvents;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using MassTransit;
using Search.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Search.Infrastructure.Consumers
{
    public class CatalogItemAddedEventConsumer(ElasticsearchClient elasticsearchClient)
        : IConsumer<CatalogItemAddedEvent>
    {

        private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

        public async Task Consume(ConsumeContext<CatalogItemAddedEvent> context)
        {
            var message = context.Message;

            if (message == null) return;

            var itemIndex = new CatalogItemIndex
            {
                CatalogBrand = message.CatalogBrand,
                CatalogCategory = message.CatalogCategory,
                Description = message.Description,
                DetailUrl = message.DetailUrl,
                Name = message.Name,
                Slug = message.Slug,
            };


            var result = await _elasticsearchClient.Indices.ExistsAsync(CatalogItemIndex.IndexName);
            if ( !result.Exists)
            {
              await  _elasticsearchClient.Indices.CreateAsync(index: CatalogItemIndex.IndexName);


            }

            var response = await _elasticsearchClient.IndexAsync(itemIndex, x => x.Index(CatalogItemIndex.IndexName));

           
        }
    }
}



