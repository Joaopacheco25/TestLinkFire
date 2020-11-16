using System.Collections.Generic;
using DataIngestion.TestAssignment.Models;
using MediatR;
using BulkResponse = Nest.BulkResponse;

namespace DataIngestion.TestAssignment.Mediator
{
    public class CollectionCommand:IRequest<BulkResponse>
    {
        public IEnumerable<Album> AlbumCollection { get; set; }
    }
}