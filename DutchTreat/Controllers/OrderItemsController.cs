using AutoMapper;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(
            IDutchRepository repository,
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order == null || order.Items == null)
            {
                _logger.LogInformation("Failed to get order");
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order == null || order.Items == null)
                return NotFound();

            var item = order.Items?.Where(i => i.Id == id).FirstOrDefault();
            if (item == null)
            {
                _logger.LogInformation("Failed to get order");
                return NotFound($"Item not found with id: {id}");
            }

            return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
        }
    }
}
