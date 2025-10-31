using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;

namespace StoreApp.Services
{
    class StoreService : IStoreService
    {
        private readonly IDataManager _dataManager;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        private List<Category> _categories;
        private List<Product> _products;
        public List<Product> GetAllProducts() => _products;
        public List<Product> Products { get; set; } = new List<Product>();

        public StoreService(IDataManager dataManager, ICartService cartService, IOrderService orderService)
        {
            _dataManager = dataManager;
            _cartService = cartService;
            _orderService = orderService;

            _products = _dataManager.LoadProducts();
            _categories = _dataManager.LoadCategories();
        }

        public void ShowCategories(UserAccount user)
        {
            while (true)
            {
                var categoryOptions = _categories
                    .Select(c => $"{c.Id}:{c.Name}")
                    .Append("Назад")
                    .ToArray();

                int choice = UIHelper.MenuSelect(categoryOptions);

                if (choice == categoryOptions.Length - 1)
                {
                    break;
                }

                // Console.WriteLine("Категория товаров:");
                var selectedCategory = _categories.ElementAt(choice);
                ShowProductsByCategory(selectedCategory.Id, user);
            }
            //foreach (var p  in _categories)
            //Console.WriteLine($"{p.Id}:{p.Name}");


        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
            _dataManager.SaveProducts(_products);
        }

        public void ShowProductsByCategory(int categoryId, UserAccount user)
        {
            var products = _products.Where(p => p.CategoryId == categoryId).ToList();

            if (!products.Any())
            {
                Console.WriteLine("Нет товаров в этой категории");
                return;
            }

            Console.WriteLine($"Товары категории: {_categories.First(c => c.Id == categoryId).Name}");
            var countProduct = products
                .Select(p => $"{p.Id}:{p.Name} - {p.Price}руб (Остаток {p.Quantity})")
                .Append("Назад")
                .ToArray();
            int choice = UIHelper.MenuSelect(countProduct);

            if (choice == countProduct.Length - 1)
            {
                ShowCategories(user);
                return;
            }

            var selectedProduct = products[choice];

            ShowProductsActions(selectedProduct, user);
            //foreach (var p in products)
            //{
            //    Console.WriteLine($"{p.Id}:{p.Name} - {p.Price}руб (Остаток {p.Quantity})");
            //}


        }

        public void ShowProductsActions(Product product, UserAccount user)
        {
            while (true)
            {
                int action = UIHelper.MenuSelect(new[]
                {
                    $"Добавить \"{product.Name}\" в корзину",
                    $"Купить сейчас ({product.Price} руб)",
                    "Назад"
                }, $"Товар: {product.Name}");

                switch (action)
                {
                    case 0:
                        _cartService.AddToCart(user, product.Id, 1);
                        Console.WriteLine("\n Нажмите любую кнопку, чтобы вернуться...");
                        Console.ReadKey(true);
                        return;
                    case 1:
                        _orderService.PlaceOrder(user, product.Id, 1);
                        Console.WriteLine("Заказ оформлен");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        return;

                }




            }
        }
    }

}