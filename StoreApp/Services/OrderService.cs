using StoreApp.Interface;
using StoreApp.Models;

namespace StoreApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDataManager _dataManager;
        private readonly IStoreService _storeService;
        private List<Order> _orders;

        public int Id { get; set; }
        public string Username { get; set; } // кто сделал заказ
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public OrderService(IDataManager dataManager)
        {
            _dataManager = dataManager;
           
            _orders = _dataManager.LoadOrders();
        }

        //public void AddToCart(UserAccount user, int productId, int quantity)
        //{
        //    var product = _storeService.GetAllProducts().FirstOrDefault(p => p.Id == productId);

        //    if (product == null)
        //    {

        //    }
        //}

        //public void RemoveFromCart(UserAccount user, int productId)
        //{
        //    var item = user.Cart.FirstOrDefault(c => c.ProductId == productId);
        //    if (item != null)
        //    {
        //        user.Cart.Remove(item);
        //        Console.WriteLine("🗑️ Товар удалён из корзины.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("❌ Товар не найден в корзине.");
        //    }

        //}

        //public void ViewCart(UserAccount user)
        //{
        //    if(!user.Cart.Any())
        //    {
        //        Console.WriteLine("🛒 Корзина пуста.");
        //        return;
        //    }

        //    Console.WriteLine("");
        //    foreach (var item in user.Cart)
        //    {
        //        var product = _storeService.GetAllProducts().FirstOrDefault(p => p.Id == item.ProductId);
        //        Console.WriteLine($"{product.Name} - {item.Quantity} {product.Price} = {item.Quantity * product.Price}");
        //    }

        //}

        public async Task PlaceOrder(UserAccount user, int productId, int quantity)
        {
            var products = _dataManager.LoadProducts();
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine("❌ Товар не найден.");
                return;
            }

            if (product.Quantity < quantity)
            {
                Console.WriteLine("Нет товара!");
                return;
            }

            await Task.Run(() =>
                {

                    var orders = _dataManager.LoadOrders();
                    // создаём заказ
                    var order = new Order
                    {
                        Username = user.Username,
                        ProductId = productId,
                        Quantity = quantity,
                        Date = DateTime.Now,
                        TotalPrice = product.Price * quantity
                    };


                    // сохраняем заказ в user (локально)

                    
                    orders.Add(order);
                    _dataManager.SaveOrders(orders);

                    product.Quantity -= quantity;
                    _dataManager.SaveProducts(products);
                });
        
        }

    }

}
