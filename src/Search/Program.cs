using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.TransformManagement;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Search;
using Search.Infrastructure.Extensions;
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

app.Run();






//builder.Services.AddScoped(sp =>
//{
//    var elasticSettings = sp.GetRequiredService<IOptions<AppSettings>>().Value.ElasticSearchOption;

//    var settings = new ElasticsearchClientSettings(new Uri(elasticSettings.Host))
//     .CertificateFingerprint("105cc986165c01d6fe7a2ac52bdc0561220022badc0700d5a0e81684949e4e12")
//     .Authentication(new BasicAuthentication("elastic", "M@f0015795810"));
//    return new ElasticsearchClient(settings);
//});