using System;
using System.Threading;
using System.Threading.Tasks;
using DataIngestion.TestAssignment.Mediator;
using DataIngestion.TestAssignment.Utils;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace DataIngestion.TestAssignment
{
    public class LinkFireConsole: IHostedService
    {
        private IMediator _mediator;
        private ExtractFileHelper _file;
        private FileHelper _fileHelper;
        
        public LinkFireConsole(IMediator mediator)
        {
            _mediator = mediator;
            _file = new ExtractFileHelper();
            _fileHelper = new FileHelper();
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var id = new FileDownloadCommand
            {
                Id = "1AJ7icRJ5dfbWlQORocfrLhVyMOd242sm"
            };

            var result = await _mediator.Send(id, cancellationToken);

            await _file.Extract(await result.Content.ReadAsByteArrayAsync());

            var collections = new CollectionCommand()
            {
                AlbumCollection = _fileHelper.ParseFile()
            };

            await _mediator.Send(collections, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}