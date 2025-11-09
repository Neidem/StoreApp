using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Log;
using StoreApp.Models;

namespace StoreApp.Services
{
    public class AdminStoreService : IAdminStoreService
    {

        private readonly IDataManager _dataManager;
        private readonly ILogger _logger;

        public AdminStoreService(IDataManager dataManager, ILogger logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        public List<Product> GetAllProducts()
        {
            return _dataManager.LoadProducts();
        }


        public bool ShowCategories(UserAccount user)
        {
            var categories = _dataManager.LoadCategories();
            Console.WriteLine("Категория товаров [АДМИНИСТРАТОР]");
            foreach (var c in categories)
            {
                Console.WriteLine($"{c.Id}:{c.Name}");
            }
            Console.WriteLine("\n Назад");
            return true;
        }

        public bool ShowProductsByCategory(int catId, UserAccount user)
        {
            var products = _dataManager.LoadProducts().Where(p => p.CategoryId == catId).ToList();
            if (!products.Any())
            {
                Console.WriteLine("Нет товаров в данной категории");
                return false;
            }

            Console.WriteLine($"\n Товары в категории {catId}");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}:{p.Name} - {p.Price} руб (Остаток: {p.Quantity}");


            }
            return true;
            ;
        }

        public void CreateProductInteractive()
        {
            ConsoleHelper.ClearUIArea(3);
            Console.WriteLine("Добавление товара: \n");

            Console.Write("Введите название товара");
            string name = Console.ReadLine();

            Console.Write("Введите цену:");
            decimal.TryParse(Console.ReadLine(), out decimal price);

            Console.Write(" Введите количество:");
            int.TryParse(Console.ReadLine(), out int quantity);

            Console.Write("Введите ID категории:");
            int.TryParse(Console.ReadLine(), out int categoryId);

            AddProduct(name, price, quantity, categoryId);
        }

        private void AddProduct(string name, decimal price, int quantity, int categoryId)
        {
            var products = _dataManager.LoadProducts();
            var newProduct = new Product
            {

                Id = products.Any() ? products.Max(p => p.Id) + 1 : 1,
                Name = name,
                Price = price,
                Quantity = quantity
            };

            products.Add(newProduct);

            _dataManager.SaveProducts(products);

            Console.WriteLine($"Товар {name} успешно добавлен");
            Console.ReadKey();
        }

        public bool EditProductInteractive()
        {
            ConsoleHelper.ClearUIArea(3);
            
            if (!UIHelper.TryReadInt("Введите ID товара", out int id))
                return false;
            var products = _dataManager.LoadProducts();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                Console.ReadKey();
                return false;
            }
            while (true)
            {
                ConsoleHelper.ClearUIArea(3);

                int choice = UIHelper.MenuSelect(new[]
                {
                    "Изменить имя", //0
                    "Изменить категорию",
                    "Изменить цену",
                    "Изменить количество",
                    "Удалить товар", //4
                    "Назад"

        }, $"Редактирование товара:\n{product.Name} (ID: {product.Id})?");

                switch (choice)
                {
                    case 0:
                        EditNameProduct(product);
                        break;
                    case 1:
                        EditProductCategory(product);
                        break;
                    case 2:
                        EditProductPrice(product);
                        break;
                    case 3:
                        EditProductQuantity(product);
                        break;
                    case 4:
                        RemoveProduct(product.Id);
                        break;
                    case 5:
                        return true;
                }
                return false;
            }

        }

        private void EditNameProduct(Product product)
        {
            Console.WriteLine($"Редактирование {product.Name} \n");
            Console.WriteLine("Введите новое имя товара");
            string newName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                product.Name = newName;
                SaveProductChange(product);
                Console.WriteLine($"Имя товара изменено на {newName} ");
            }

            else
            {
                Console.WriteLine("Имя не может быть пустым");
            }
            Console.ReadKey();
        }

        private void EditProductCategory(Product product)
        {
            Console.WriteLine("Введите новый ID категории");
          

            if (int.TryParse(Console.ReadLine(),out int newCategory))
            {
                product.CategoryId = newCategory;
                SaveProductChange(product);
                Console.WriteLine($"Имя товара изменено на {newCategory} ");
            }

            else
            {
                Console.WriteLine("Неверный формат (нужно вводить число");
            }
            Console.ReadKey();
        }

        private void EditProductPrice(Product product)
        {
            Console.WriteLine("Введите новую цену:");
            if(decimal.TryParse(Console.ReadLine(), out decimal newPrice))
            {
                decimal oldPrice = product.Price;
                product.Price = newPrice;
                SaveProductChange(product);
                Console.WriteLine($"Цена изменена с {oldPrice}руб на {newPrice}руб");
            }
            else
            {
                Console.WriteLine("Некорректное значение цены");
            }
            Console.ReadKey();
        }

        private void EditProductQuantity(Product product)
        {
            Console.WriteLine("");
            

            if (int.TryParse(Console.ReadLine(), out int newQuantity))
            {
                int oldQty = product.Quantity;
                product.Quantity = newQuantity;
                SaveProductChange(product);
                Console.WriteLine($"Остаток товара {product.Name} изменен c {oldQty} на {newQuantity} ");

            }
            else
            {
                Console.WriteLine("Неккоректное значение!");
            }

            Console.ReadKey();

        }

        public bool IdRemove()
        {
            Console.WriteLine("Введите Id товар ");
            int.TryParse(Console.ReadLine(), out int id);

            return RemoveProduct(id);
        }

        private bool RemoveProduct(int productId)
        {
            var products = _dataManager.LoadProducts();
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine("❌ Товар не найден. Нажмите любую клавишу чтобы продолжить.");
                Console.ReadKey();
                return false;
            }

            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
            " Да, удалить товар",
            " Отменить удаление"
        }, $"Вы уверены, что хотите удалить товар:\n{product.Name} (ID: {product.Id})?");

                switch (choice)
                {
                    case 0:
                        products.Remove(product);
                        _dataManager.SaveProducts(products);
                        Console.WriteLine($"\n Товар '{product.Name}' удалён успешно.");
                        Console.ReadKey();
                        _logger.Info(LogEvents.DeleteProduct(product.Name));
                        return true;

                    case 1:
                        Console.WriteLine("\n Операция отменена.");
                        Console.ReadKey();
                        return false;
                }
            }
        }

        private void SaveProductChange(Product updateProduct)
        {
            var products = _dataManager.LoadProducts();
            var index = products.FindIndex(p => p.Id == updateProduct.Id);

            if(index != -1)
            {
                products[index] = updateProduct;
                _dataManager.SaveProducts(products);
            }

        }

    }
}
