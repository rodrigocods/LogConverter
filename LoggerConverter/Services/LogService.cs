using LoggerConverter.Repository.Interfaces;
using LoggerConverter.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggerConverter.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LogService(ILogRepository logRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logRepository = logRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> DownloadFileContent(string uri)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode) throw new ArgumentException("Não foi possível baixar as informações do arquivo informado.");

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> SaveLog(string content)
        {
            var filepath = _configuration.GetValue<string>("PathToSaveFiles");

            if (!Directory.Exists(filepath)) Directory.CreateDirectory(filepath);

            filepath += $"/{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Guid.NewGuid()}.txt";

            await File.WriteAllTextAsync(filepath, content);

            return filepath;
        }

        public string ConvertLog(string logContent)
        {
            var lines = logContent.Split("\n");

            string outputContent =
                "#Version: 1.0\n" +
                $"#Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\n" +
                $"#Fields: provider http-method status-code uri-path time-taken response-size cache-status\n";


            foreach (var line in lines)
            {
                var lineSplitted = line.Split('|');

                if (lineSplitted.Length < 2) continue;

                var thirdSplitted = lineSplitted[3].Split(" ");

                string method = thirdSplitted[0].Substring(1, thirdSplitted[0].Length - 1);

                string uri = thirdSplitted[1];

                string statusCode = lineSplitted[1];

                string timeTaken = (Math.Round(Convert.ToDecimal(lineSplitted[4]))).ToString();

                string responseSize = lineSplitted[0];

                string cacheStatus = lineSplitted[2];

                outputContent += $"\"MINHA CDN\" {method} {statusCode} {uri} {timeTaken} {responseSize} {cacheStatus}\n";
            }

            return outputContent;
        }

        public async Task<string> SaveLogConverted(string outputContent)
        {
            var outputFilepath = (_configuration.GetValue<string>("PathToSaveFiles") + "/converted");
            if (!Directory.Exists(outputFilepath)) Directory.CreateDirectory(outputFilepath);

            outputFilepath += $"/{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Guid.NewGuid()}.txt";

            await File.WriteAllTextAsync(outputFilepath, outputContent);

            return outputFilepath;
        }
    }
}