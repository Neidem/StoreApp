using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Services;


namespace StoreApp.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; } // имя
        public decimal Price { get; set; } //цена
     //   public int Stock { get; set; }
     //   public string Category { get; set; } //категория

        public int Quantity { get; set; } //количество на складе
    }
}
