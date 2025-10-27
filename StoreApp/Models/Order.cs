using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Username { get; set; } // кто сделал заказ
        public List<Product> Products { get; set; } = new();
        public decimal TotalAmount => Products.Sum(p => p.Price);
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
