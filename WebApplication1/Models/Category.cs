using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
 
    public class Category: BaseEntity
    {
        public string Name { get; set; } = "";
        public int ParentId { get; set; }

        [NotMapped]
        public List<Book>? Books { get; set; } = new();
    }
}
