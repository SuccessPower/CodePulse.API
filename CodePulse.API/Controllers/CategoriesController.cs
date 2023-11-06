using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Models
            var category = new Category
            { 
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            
            await categoryRepository.CreateAsync(category);

            // Map Domain Models to Dtos
            var response = new CategoryDto
            {   
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
             
            return Ok(response);
        }

        // Get: https://localhost:7263/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            // Map model to DTO
            var response = new List<CategoryDto>();
            foreach (var category in categories) 
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }

        // Get: https://localhost:7263/api/Category/{Guid:id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var existingResponse = await categoryRepository.GetById(id);

            if (existingResponse is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existingResponse.Id,
                Name = existingResponse.Name,
                UrlHandle = existingResponse.UrlHandle,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult> EditCategory([FromRoute] Guid id, EditCategoryDto editCategoryDto)
        {
            var category = new Category
            {
                Id = id,
                Name = editCategoryDto.Name,
                UrlHandle = editCategoryDto.UrlHandle,
            };

            category = await categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            // Convert Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            
            return Ok(response);
        }
    }
}
