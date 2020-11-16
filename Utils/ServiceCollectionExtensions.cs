using DataIngestion.TestAssignment.Models;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using Microsoft.Extensions.Configuration;

namespace DataIngestion.TestAssignment.Utils
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, string indexConfiguration)
        {
             var url = "http://localhost:9200";;
             var index = indexConfiguration;

             var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(index)
                 .DefaultMappingFor<Album>(x => x.IndexName(index));

             var client = new ElasticClient(settings);

             serviceCollection.AddSingleton(client);

             client.Indices.Create(index, idx =>
                 idx.Map<Album>(x => x.AutoMap()));
             
            
             return serviceCollection;
        }
    }
}