using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
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
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            // Map Domain Models to Dtos
            var response = new CategoryDto
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            return Ok(response);
        }
    }
}
