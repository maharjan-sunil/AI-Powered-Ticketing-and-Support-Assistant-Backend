using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
//using UserService.API.RabbitMQ;

namespace DockerDemo.Docker.Controller
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _service.GetCategoryAsync();
            return Ok(categories);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _service.AddCategoryAsync(category);
            return NoContent();

        }

        [HttpPatch]
        //[Authorize]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var updatedCategory = await _service.UpdateCategoryAsync(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);

        }

        [HttpDelete]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _service.DeleteCategoryAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetCategoryById(int id)
        {
            var category =  await _service.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }   
            return Ok(category);
        }
    }
}

