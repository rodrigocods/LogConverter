using LoggerConverter.Data;
using LoggerConverter.Models;
using LoggerConverter.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerConverter.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly LogContext _context;

        public LogRepository(LogContext context)
        {
            _context = context;
        }

        public async Task<List<Log>> GetAll(int page = 1, int pageSize = 10)
        {
            return await _context.Logs
                .Skip(pageSize * (page-1))
                .Take(1)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<Log> Find(int id)
        {
            return await _context.Logs.FindAsync(id);
        }

        public async Task Add(Log log)
        {
            await _context.Logs.AddAsync(log);
        }
    }
}