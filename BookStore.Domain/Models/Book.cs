using Newtonsoft.Json;

namespace BookStore.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }

        [JsonIgnore]
        public decimal Total { get {
                return UnitPrice - (UnitPrice * DiscountPercentage);
            } 
        }
    }
}
