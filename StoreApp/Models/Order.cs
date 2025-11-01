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
        public string Username { get; set; }
        public int Quantity {  get; set; }
        public int ProductId { get; set; }  
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
