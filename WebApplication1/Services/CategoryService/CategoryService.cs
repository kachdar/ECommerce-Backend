using AutoMapper;
using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Models;

namespace WebApplication1.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;
        public CategoryService(PostgreSqlContext context, IMapper mapper) 
        {
            postgreSqlContext= context;
            this.mapper = mapper;
        }
        public async Task<List<GetCategoryDto>> GetCategoriesByParentId(int parentId)
        {
            var categories = await postgreSqlContext.Categories.Where(c => c.ParentId == parentId).ToListAsync();
            return categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToList();
        }

        public async Task<List<GetBookDto>> GetBooksByCategoryId(int id)
        {
            List<int> categoryIds = await postgreSqlContext.Categories.Where(c => c.ParentId == id).Select(c => c.Id).ToListAsync();

            if (categoryIds.Count > 0)
            {
                return postgreSqlContext.Books.Where(b => categoryIds.Contains(b.CategoryId)).Select(b => mapper.Map<GetBookDto>(b)).ToList();
            }
            else 
            {
                return postgreSqlContext.Books.Where(b => b.CategoryId == id).Select(b => mapper.Map<GetBookDto>(b)).ToList();
            }
        }

        public async Task<List<GetCategoryDto>> AddCategory(AddCategoryDto addCategoryDto)
        {
            postgreSqlContext.Categories.Add(mapper.Map<Category>(addCategoryDto));
            await postgreSqlContext.SaveChangesAsync();
            return postgreSqlContext.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToList(); ;
        }

        public async Task<List<GetCategoryDto>?> UpdateCategory(AddCategoryDto addCategoryDto)
        {
            var category = await postgreSqlContext.Categories.FindAsync(addCategoryDto.Id);
            if (category is null)
                return null;

            category.Name = addCategoryDto.Name;

            await postgreSqlContext.SaveChangesAsync();

            return postgreSqlContext.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToList();
        }
    }
}
