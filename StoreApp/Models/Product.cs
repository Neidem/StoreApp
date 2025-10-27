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
        public string Name { get; set; }
        public decimal Price { get; set; } //цена 
        public int Quantity { get; set; } // количество на складе
        public int CategoryId { get; set; } // Связь с категорией//количество на складе
    }
}
