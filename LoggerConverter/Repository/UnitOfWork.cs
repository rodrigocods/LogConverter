using LoggerConverter.Data;
using LoggerConverter.Repository.Interfaces;
using System.Threading.Tasks;

namespace LoggerConverter.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogContext _context;

        public UnitOfWork(LogContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
