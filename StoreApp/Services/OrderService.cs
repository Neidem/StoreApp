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

        public void PlaceOrder(string username, int productId, int quantity)
        {
            var order = new Order { Username = username, ProductId = productId, Quantity = quantity, Date = DateTime.Now };
           // _dataManager.SaveOrder(order);
        }


    }

}
