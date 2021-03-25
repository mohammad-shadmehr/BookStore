using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Interfaces;
using BookStore.Application.Validators;
using BookStore.Application.DTOs;
using BookStore.Domain.Service;

namespace BookStore.Application.Implementations
{
    public class ApplicationService : IApplicationService
    {
        private readonly IEnumerable<IValidator<string>> _inputValidators;
        private readonly IRepository<Book> _bookRepository;

        public ApplicationService(
            IEnumerable<IValidator<string>> inputValidators,
            IBookRepository bookRepository
            )
        {
            _inputValidators = inputValidators;
            _bookRepository = bookRepository;
        }

        public async Task<CalculateOutput> CalculateAsync(IList<OrderItem> input)
        {
            var output = new CalculateOutput();
            var books = _bookRepository.GetAll()
                                       .Where(b => input.Any(o => o.BookId == b.Id))
                                       .ToList();

            foreach (var order in input)
            {
                var book = books.Single(b => b.Id == order.BookId);

                output.Total += book.Total * order.Quantity;
            }

            if (output.TotalWithGST < BookStoreConsts.DELIVERY_FEE_MINIMUM_ORDER)
            {
                output.Total += BookStoreConsts.DELIVERY_FEE;
                output.DeliveryFee = BookStoreConsts.DELIVERY_FEE;
            }

            return output;
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();

            return books.Select(b => new BookDto() { 
                        Id = b.Id,
                        Author = b.Author,
                        DiscountPercentage = b.DiscountPercentage,
                        Genre = b.Genre,
                        Title = b.Title,
                        UnitPrice = b.UnitPrice
                    }).ToList();
        }

        public Validation ValidateInput(IList<string> input)
        {
            foreach (var validator in _inputValidators)
            {
                foreach (var i in input)
                {
                    var r = validator.IsValid(i);
                    if (!r.IsValid)
                    {
                        return r;
                    }
                }
            }
           
            return new Validation() {IsValid = true};
        }

    }
}
