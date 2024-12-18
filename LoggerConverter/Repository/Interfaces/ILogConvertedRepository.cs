using LoggerConverter.Dtos;
using LoggerConverter.Models;
using System.Threading.Tasks;

namespace LoggerConverter.Repository.Interfaces
{
    public interface ILogConvertedRepository
    {
        Task<LogsConvertedDashboardDto> GetDashboard(int page = 1, int pageSize = 10);

        Task<LogConverted> Find(int id);

        Task Add(LogConverted logConverted);
    }
}