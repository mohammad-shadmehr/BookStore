using BookStore.Domain.Models;

namespace BookStore.Infrastructure.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}