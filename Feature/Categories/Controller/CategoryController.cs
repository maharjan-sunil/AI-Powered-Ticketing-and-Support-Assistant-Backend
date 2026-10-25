using AI_Powered_Ticketing_and_Support_System.Feature.Categories.DTO;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
//using UserService.API.RabbitMQ;

namespace DockerDemo.Docker.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _service;
        private readonly IValidator<CreateCategoryRequest> _validator;

        public CategoryController(ICategoryService service, IValidator<CreateCategoryRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _service.GetCategoryAsync();
            return Ok(categories);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary()); // Aborts early if invalid
            }

            // 2. Map the validated DTO safely into your real Database Entity
            var categoryEntity = new Category
            {
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                IsActive = true, // Force-set by backend logic, immune to user override
               // Tickets = new List<Ticket>() // Safely initialized empty
            };


            await _service.AddCategoryAsync(categoryEntity);
            return NoContent();

        }

        [HttpPatch]
        //[Authorize]
        public async Task<IActionResult> UpdateCategory(CreateCategoryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary()); // Aborts early if invalid
            }

            // 2. Map the validated DTO safely into your real Database Entity
            var categoryEntity = new Category
            {
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                IsActive = request.IsActive
            };

            var updatedCategory = await _service.UpdateCategoryAsync(categoryEntity);
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

