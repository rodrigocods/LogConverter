using LoggerConverter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerConverter.Repository.Interfaces
{
    public interface ILogRepository
    {
        Task<List<Log>> GetAll(int page = 1, int pageSize = 10);

        Task<Log> Find(int id);

        Task Add(Log log);
    }
}