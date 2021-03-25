using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Interfaces;
using BookStore.Infrastructure.Helpers;

namespace BookStore.Infrastructure.Repositories
{
    public class BookJsonRepository : IBookRepository
    {
        public IEnumerable<Book> GetAll()
        {
            var path = "books.json";
            var data = FileHelper.Read(FileHelper.PersistancePath + path);
            var result = JsonHelper.Deserialise<IEnumerable<Book>>(data);

            return result;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var path = "books.json";
            var data = FileHelper.Read(FileHelper.PersistancePath + path);
            var result = JsonHelper.Deserialise<IEnumerable<Book>>(data);

            return result;
        }
    }
}
