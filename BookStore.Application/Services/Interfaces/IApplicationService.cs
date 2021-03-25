using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.DTOs;
using BookStore.Application.Validators;

namespace BookStore.Application.Interfaces
{
    public interface IApplicationService
    {
        Validation ValidateInput(IList<string> input);
        Task<CalculateOutput> CalculateAsync(IList<OrderItem> input);
        Task<List<BookDto>> GetAllBooks();
    }
}
