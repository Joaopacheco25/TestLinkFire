using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DataIngestion.TestAssignment.Mediator;
using DataIngestion.TestAssignment.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		
		static async Task Main(string[] args)
		{
		
			var serviceProvider = new ServiceCollection()
				.AddHttpClient()
				.AddServices("albums")
				.AddSingleton<IHostedService, LinkFireConsole>()
				.AddMediatR(Assembly.GetExecutingAssembly())
				.BuildServiceProvider();

			var mediatr = serviceProvider.GetService<IMediator>();
			await new LinkFireConsole(mediatr).StartAsync(CancellationToken.None);
		}
	}
}
