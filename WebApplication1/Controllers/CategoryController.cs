using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Services.BookService;
using WebApplication1.Services.CategoryService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var result = await postgreSqlContext.Categories.ToListAsync();
            return Ok(result);
        }*/

        [HttpGet("{parentId}")]
        public async Task<ActionResult<List<GetCategoryDto>>> GetSubcategoriesByParentId(int parentId)
        {
            var result = await service.GetCategoriesByParentId(parentId);
            return Ok(result);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<List<GetBookDto>>> GetBooksByCategoryId(int id)
        {
            var result = await service.GetBooksByCategoryId(id);

            if (result is null)
                return NotFound("There is no book");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCategoryDto>>> AddCategory(AddCategoryDto addCategoryDto)
        {
            var result = await service.AddCategory(addCategoryDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<GetCategoryDto>>> UpdateCategory(AddCategoryDto addCategoryDto)
        {
            var result = await service.UpdateCategory(addCategoryDto);
            if (result is null)
                return NotFound("Category doesn't exist");

            return Ok(result);
        }
    }
}
