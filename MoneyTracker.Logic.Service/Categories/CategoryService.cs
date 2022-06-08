using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Logic.Categories;
using MoneyTracker.Storage;
using MoneyTracker.Storage.Models.Entities;
using System.Security.Authentication;

namespace MoneyTracker.Logic.Service.Categories;

public class CategoryService : ICategoryService
{
    private readonly MoneyTrackerContext _context;
    private readonly IMapper _mapper;

    public CategoryService(MoneyTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateUpdateRequest request, int userId)
    {
        var category = _mapper.Map<Category>(request);
        category.CreatedBy = userId;
        category.CreatedAt = DateTime.UtcNow;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        category = await GetAllowedAsync(category.Id, userId);
        var categoryDto = _mapper.Map<CategoryDto>(category);
        categoryDto.IsCreatedByMe = category.CreatedBy == userId;

        return categoryDto;
    }

    public async Task<CategoryDto> GetAllowedForUserAsync(int id, int userId)
    {
        var categoryDtos = await GetForUserAsync(userId);

        return categoryDtos.FirstOrDefault(c => c.Id == id)
               ?? throw new KeyNotFoundException($"The Selected Category is Identifier '{id}' does not exist.");
    }

    public async Task<IList<CategoryDto>> GetForUserAsync(int userId)
    {
        var categories = await _context
            .Categories
            .Where(c => c.IsShared || c.CreatedBy == userId)
            .ToListAsync();

        var categoryDtos = _mapper.Map<IList<CategoryDto>>(categories);

        categories.ForEach(category =>
        {
            var categoryDto = categoryDtos.First(dto => dto.Id == category.Id);
            categoryDto.IsCreatedByMe = category.CreatedBy == userId;
        });

        return categoryDtos;
    }

    public async Task<CategoryDto> UpdateAsync(CategoryCreateUpdateRequest request, int userId)
    {
        var category = await GetAllowedAsync(request.Id, userId);

        category.Name = request.Name;
        category.Description = request.Description;
        category.IsShared = request.IsShared;

        await _context.SaveChangesAsync();

        category = await GetAllowedAsync(category.Id, userId);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var category = await GetAllowedAsync(id, userId);

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    private async Task<Category> GetAllowedAsync(int id, int userId)
    {
        var category = await GetOrNotFoundAsync(id);

        if (category.CreatedBy != userId)
            throw new AuthenticationException($"You do not have access to the selected Category with Identifier '{id}'");

        return category;
    }

    private async Task<Category> GetOrNotFoundAsync(int id) =>
        await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id)
        ?? throw new KeyNotFoundException($"Category with Identifier '{id}' not found.");
}