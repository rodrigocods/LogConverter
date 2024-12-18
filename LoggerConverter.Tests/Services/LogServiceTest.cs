using LoggerConverter.Repository.Interfaces;
using LoggerConverter.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Net.Http;
using Xunit;

namespace LoggerConverter.Tests.Services
{
    public class LogServiceTest
    {
        [Fact]
        public void ConvertLog_Success_LogConverted()
        {
            // Arrange
            var logRepository = Substitute.For<ILogRepository>();
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();
            var logService = new LogService(logRepository, httpClientFactory, configuration);

            string inputLogContent = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2\n" +
                "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4\n" +
                "199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9\n" +
                "312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1";

            string expectedOutput = "#Version: 1.0\n" +
                "#Date: 17/12/2024 17:12:46\n" +
                "#Fields: provider http-method status-code uri-path time-taken response-size cache-status\n" +
                "\"MINHA CDN\" GET 200 /robots.txt 1002 312 HIT\n" +
                "\"MINHA CDN\" POST 200 /myImages 3194 101 MISS\n" +
                "\"MINHA CDN\" GET 404 /not-found 1429 199 MISS\n" +
                "\"MINHA CDN\" GET 200 /robots.txt 2451 312 INVALIDATE\n";

            // Act
            var outputLogContent = logService.ConvertLog(inputLogContent);

            // Assert
            Assert.Equal(expectedOutput.Substring(45, 250), outputLogContent.Substring(45, 250));
        }
    }
}
