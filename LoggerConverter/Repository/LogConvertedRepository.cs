using LoggerConverter.Data;
using LoggerConverter.Dtos;
using LoggerConverter.Models;
using LoggerConverter.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerConverter.Repository
{
    public class LogConvertedRepository : ILogConvertedRepository
    {
        private readonly LogContext _context;

        public LogConvertedRepository(LogContext context)
        {
            _context = context;
        }

        public async Task<LogsConvertedDashboardDto> GetDashboard(int page = 1, int pageSize = 10)
        {
            return new LogsConvertedDashboardDto()
            {
                LogsDashboard = await _context.LogsConverted
                    .Include(l => l.Log)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .OrderByDescending(l => l.CreatedAt)
                    .Select(lc =>  new LogsConvertedDashboardItemDto() { LogConverted = lc, Log = lc.Log })
                    .ToListAsync()
            };
        }

        public async Task<LogConverted> Find(int id)
        {
            return await _context.LogsConverted.FindAsync(id);
        }

        public async Task Add(LogConverted logConverted)
        {
            await _context.LogsConverted.AddAsync(logConverted);
        }
    }
}