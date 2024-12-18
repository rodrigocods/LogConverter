using System.Threading.Tasks;

namespace LoggerConverter.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
