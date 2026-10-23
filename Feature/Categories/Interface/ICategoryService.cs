using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;

namespace AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Interface
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoryAsync();
        public Task AddCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
        public Task<Category?> GetCategoryByIdAsync(int id);
        public Task<Category?> UpdateCategoryAsync(Category category);
    }
}
