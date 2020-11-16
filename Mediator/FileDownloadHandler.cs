using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace DataIngestion.TestAssignment.Mediator
{
    public class FileDownloadHandler:IRequestHandler<FileDownloadCommand,HttpResponseMessage>
    {
        private readonly HttpClient _client;
        
        public FileDownloadHandler(IHttpClientFactory client)
        {
            _client = client.CreateClient();
            _client.BaseAddress = new Uri("https://drive.google.com");
        }
        
        public async Task<HttpResponseMessage> Handle(FileDownloadCommand request, CancellationToken cancellationToken)
        {
                  
            var response = await _client.GetAsync($"/uc?export=download&id={request.Id}", cancellationToken);

            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            
            var text = Encoding.Default.GetString(await response.Content.ReadAsByteArrayAsync());
            return await _client.GetAsync($"/uc?export=download&id={request.Id}&confirm=" +
                                          $"{GetQueryValue(text)}", cancellationToken);
        }
        
        private static string GetQueryValue(string text)
        {
            string query = "confirm=";
            int start, end;
            start = text.IndexOf(query, 0, StringComparison.Ordinal) + query.Length;
            end = text.IndexOf("&", start, StringComparison.Ordinal);
            return text[start..end];
        }
    }
}