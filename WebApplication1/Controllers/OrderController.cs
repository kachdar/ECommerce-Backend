using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Services.BookService;
using WebApplication1.Services.OrderService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService service)
        {
            orderService = service;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(CreateOrderDto order)
        {
            await orderService.CreateOrder(order);
            return Ok();
        }
    }
}
