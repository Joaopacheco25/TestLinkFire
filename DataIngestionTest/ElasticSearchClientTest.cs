using System;
using System.Collections.Generic;
using DataIngestion.TestAssignment.Mediator;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DataIngestion.TestAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Nest;
using NUnit.Framework;

namespace TestProject2
{
    public class CreateAlbumCollectionHandlerTest
    {
        private Mock<IElasticClient> _clientMock;
        private readonly TestFixture _fixture = new TestFixture();
        private Mock<ISearchResponse<Album>> _mockSearchResponse;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private FileDownloadHandler _handler;
        private FileDownloadCommand _command;

        [SetUp]
        public void Setup()
        {
            _clientMock = new Mock<IElasticClient>();
            _mockSearchResponse = new Mock<ISearchResponse<Album>>();
            _httpClientFactory = new Mock<IHttpClientFactory>();
        }

        [Test]
        public void Should_ReturnTwoAlbuns_When_List_Was_Passed_To_Documents()
        {
            //Arrange
            var collectionFixture = _fixture.GetAlbunsList().ToList();
            _mockSearchResponse.Setup(x => x.Documents)
                .Returns(collectionFixture);
            
            _clientMock.Setup(x => x
                    .Search(It.IsAny<Func<SearchDescriptor<Album>,
                        ISearchRequest>>()))
                .Returns(_mockSearchResponse.Object);

            //Act
            var result = _clientMock.Object.Search<Album>(s => s);
            
            //Assert
            Assert.AreEqual(2, result.Documents.Count());
        }
        
        [Test]
        public void Should_ReturnZeroAlbuns_When_List_Was_Passed_To_Documents()
        {
            //Arrange
            var collectionFixture = new List<Album>();
            _mockSearchResponse.Setup(x => x.Documents)
                .Returns(collectionFixture);
            
            _clientMock.Setup(x => x
                    .Search(It.IsAny<Func<SearchDescriptor<Album>,
                        ISearchRequest>>()))
                .Returns(_mockSearchResponse.Object);

            //Act
            var result = _clientMock.Object.Search<Album>(s => s);
            
            //Assert
            Assert.AreEqual(0, result.Documents.Count());
        }

        [Test]
        public async Task DownLoad_File_Command_Handler()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
                mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'name':dummy&,'city':'Lisbon'}"),
                });
                
                var client = new HttpClient(mockHttpMessageHandler.Object);
                _httpClientFactory.Setup(x => x
                .CreateClient(It.IsAny<string>())).Returns(client);
                _command = new FileDownloadCommand()
                {
                    Id = "00000ZZZZZXXX"
                };
                _handler = new FileDownloadHandler(_httpClientFactory.Object);

            //Act
            var result = await _handler.Handle(_command, new CancellationToken());
            
            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}