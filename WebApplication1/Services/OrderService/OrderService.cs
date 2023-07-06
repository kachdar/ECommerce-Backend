using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Dtos;

namespace WebApplication1.Services.OrderService
{
    public class OrderService: IOrderService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper _mapper;

        public OrderService(PostgreSqlContext context, IMapper mapper)
        {
            postgreSqlContext = context;
            _mapper = mapper;
        }
        public async Task CreateOrder(CreateOrderDto order)
        {
            int id = 22;
            var user = postgreSqlContext.Users.Find(id);

            Order newOrder = new Order();
            newOrder.TotalPrice = order.TotalPrice;
            newOrder.User = user;

            postgreSqlContext.Orders.Add(newOrder);

            foreach (var item in order.OrderItems)
            {
                var book = postgreSqlContext.Books.Find(item?.Book?.Id);

                OrderItem orderItem = new OrderItem();
                orderItem.Quantity = item.Quantity;
                orderItem.Order = newOrder;
                orderItem.Book = book;

                postgreSqlContext.OrderItems.Add(orderItem);
            }
            await postgreSqlContext.SaveChangesAsync();
        }
    }
}
