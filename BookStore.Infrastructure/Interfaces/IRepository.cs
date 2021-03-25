using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
    }
}
