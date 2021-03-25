using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs
{
    public class CalculateOutput
    {
        const decimal GST = 1.1m;

        public decimal Total { get; set; } = 0;
        public decimal TotalWithGST { 
            get {
                return Total * GST;
            } 
        }

        public decimal DeliveryFee { get; set; } = 0;
    }
}
