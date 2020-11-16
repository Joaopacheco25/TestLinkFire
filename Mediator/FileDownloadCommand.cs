using System.Net.Http;
using MediatR;


namespace DataIngestion.TestAssignment.Mediator
{
    public class FileDownloadCommand : IRequest<HttpResponseMessage>
    {
        public string Id { get; set; }
    }
}