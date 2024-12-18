using LoggerConverter.Models;
using System.Threading.Tasks;

namespace LoggerConverter.Services.Interfaces
{
    public interface ILogService
    {
        Task<string> DownloadFileContent(string uri);

        Task<string> SaveLog(string content);

        string ConvertLog(string logFilePath);

        Task<string> SaveLogConverted(string outputContent);
    }
}