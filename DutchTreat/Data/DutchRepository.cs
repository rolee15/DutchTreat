using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            _logger.LogInformation("GetAllOrders was called");
            return _context.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Products
                               .OrderBy(p => p.Title)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products", ex);
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .Where(p => p.Id == id)
                           .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                           .Where(p => p.Category == category)
                           .ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
