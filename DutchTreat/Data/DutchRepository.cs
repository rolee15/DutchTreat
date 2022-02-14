﻿using DutchTreat.Data.Entities;

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

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            _logger.LogInformation("GetAllOrders was called");

            if (includeItems)
            {
                return _context.Orders.Include(o => o.Items)
                                      .ThenInclude(i => i.Product)
                                      .ToList();
            }

            return _context.Orders.ToList();
        }

        public object GetAllOrdersByUser(string? username, bool includeItems)
        {

            _logger.LogInformation("GetAllOrders was called");

            if (includeItems)
            {
                return _context.Orders.Where(o => o.User.UserName == username)
                                      .Include(o => o.Items)
                                      .ThenInclude(i => i.Product)
                                      .ToList();
            }

            return _context.Orders.Where(o => o.User.UserName == username)
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

        public Order GetOrderById(string username, int id)
        {
            return _context.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .Where(o => o.Id == id && o.User.UserName == username)
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
