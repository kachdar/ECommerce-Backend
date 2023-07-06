namespace WebApplication1.Dtos.CategoryDtos
{
    public class AddCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int ParentId { get; set; }

    }
}
