using WebApplication1.Dtos;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services.OrderService
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto order);
    }
}
