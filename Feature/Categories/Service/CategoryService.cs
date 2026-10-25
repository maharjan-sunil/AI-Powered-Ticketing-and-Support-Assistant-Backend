using AI_Powered_Ticketing_and_Support_System.Data;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UserService.API.Security;

namespace AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDbContext _context;
        //private readonly ILogger<CommentService> _logger;
        //  private readonly AzureKey _key;

        public CategoryService(ApplicationDbContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            //    _key = key;
            //_logger = logger;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int Id)
        {
            
            if (Id != 0)
            {
                return await _context.Categories.FindAsync(Id);
            }

            return null;
        }

       
        public async Task<Category?> UpdateCategoryAsync(Category category)
        {

            var categoryDetail = await _context.Categories.FindAsync(category.Id);

            if (categoryDetail is null)
                return null;

            try
            {
                await _context.SaveChangesAsync();
                return categoryDetail;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }

    }
}
