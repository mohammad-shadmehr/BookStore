using BookStore.Application.DTOs;
using BookStore.Application.Implementations;
using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Interfaces;
using BookStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Console
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //setup our DI
            ServiceProvider serviceProvider = RegisterDependencies();

            var appService = serviceProvider.GetService<IApplicationService>();

            var allBooks = await appService.GetAllBooks();

            var order = new List<OrderItem>() {
                new OrderItem() { BookId = 1, Quantity = 1 },
                new OrderItem() { BookId = 3, Quantity = 1 },
                new OrderItem() { BookId = 4, Quantity = 1 },
                new OrderItem() { BookId = 6, Quantity = 1 },
                new OrderItem() { BookId = 7, Quantity = 1 }
            };

            var orderedBooks = allBooks.Where(b => order.Any(o => o.BookId == b.Id));
            var invoice = await appService.CalculateAsync(order);

            PrintInvoice(orderedBooks, invoice);
        }

        private static ServiceProvider RegisterDependencies()
        {
            return new ServiceCollection()
                            .AddSingleton<IBookRepository, BookJsonRepository>()
                            .AddSingleton<IApplicationService, ApplicationService>()
                            .BuildServiceProvider();
        }

        private static void PrintInvoice(IEnumerable<BookDto> orderedBooks, CalculateOutput invoice)
        {
            System.Console.WriteLine("Books Ordered:");
            System.Console.WriteLine("---------------------------------------------------");
            foreach (var book in orderedBooks)
            {
                System.Console.WriteLine("{0,-20} {1,20}",book.Title,$"${book.UnitPrice.ToString("#.##")}");
            }
            System.Console.WriteLine("---------------------------------------------------");

            System.Console.WriteLine($"Total: ${invoice.Total.ToString("#.##")} \t\t Total(inc. GST): ${invoice.TotalWithGST.ToString("#.##")} \t\t Delivery Fee: ${invoice.DeliveryFee.ToString("0.##")}");
        }
    }
}
