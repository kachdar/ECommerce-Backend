using WebApplication1.Dtos.CategoryDtos;

namespace WebApplication1.Services.CategoryService
{
    public interface ICategoryService
    { 
        Task<List<GetCategoryDto>> GetCategoriesByParentId(int parentId);
        Task<List<GetBookDto>> GetBooksByCategoryId(int id);
        Task<List<GetCategoryDto>> AddCategory(AddCategoryDto addCategoryDto);
        Task<List<GetCategoryDto>?> UpdateCategory(AddCategoryDto addCategoryDto);
    }
}
