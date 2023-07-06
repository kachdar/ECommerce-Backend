namespace WebApplication1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } = "";

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<OrderItem>? OrderItems { get; set; } = new();
        public List<Review>? Reviews { get; set; } = new();
    }
}
