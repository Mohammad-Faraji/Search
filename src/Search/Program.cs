using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.TransformManagement;
using Elastic.Transport;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Search;
using Search.Infrastructure.Extensions;
using Search.Models;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.BorokerConfigure();
builder.ElasticSearchConfigure();

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapGet(pattern: "/", SearchItem);

app.Run();


static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchItem(string qr, ElasticsearchClient elasticsearch)
{

    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
    .Indices(CatalogItemIndex.IndexName)
    .From(0)
    .Size(10)
    .Query(q => q.Fuzzy(t => t.Field(x => x.Description).Value(qr)))

     );
    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();

}


