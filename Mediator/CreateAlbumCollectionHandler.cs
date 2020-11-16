using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataIngestion.TestAssignment.Models;
using Elasticsearch.Net;
using MediatR;
using Nest;

namespace DataIngestion.TestAssignment.Mediator
{
    public class CreateAlbumCollectionHandler :IRequestHandler<CollectionCommand,BulkResponse>
    {
        private readonly ElasticClient _client;
        private BulkResponse _bulkResponse;

        public CreateAlbumCollectionHandler(ElasticClient client)
        {
            _client = client;
            _bulkResponse = new BulkResponse();
        }
        
        public async Task<BulkResponse> Handle(CollectionCommand request, CancellationToken cancellationToken)
        {
            var response = request.AlbumCollection
                .Select(x => new BulkIndexOperation<Album>(x))
                .Cast<IBulkOperation>().ToList();

            var bulkRequest = new BulkRequest()
            {
                Refresh = new Refresh(),
                Operations = response
            };

            _bulkResponse = await _client.BulkAsync(bulkRequest, cancellationToken);

            return _bulkResponse;
        }
    }
}