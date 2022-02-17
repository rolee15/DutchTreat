namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        object GetAllOrdersByUser(string? username, bool includeItems);
        Order GetOrderById(string username, int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}