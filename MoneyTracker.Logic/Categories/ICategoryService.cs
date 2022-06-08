namespace MoneyTracker.Logic.Categories;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CategoryCreateUpdateRequest request, int userId);
    Task<CategoryDto> GetAllowedForUserAsync(int id, int userId);
    Task<IList<CategoryDto>> GetForUserAsync(int userId);
    Task<CategoryDto> UpdateAsync(CategoryCreateUpdateRequest request, int userId);
    Task DeleteAsync(int id, int userId);
}