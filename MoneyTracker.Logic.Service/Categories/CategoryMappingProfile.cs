using AutoMapper;
using MoneyTracker.Logic.Categories;
using MoneyTracker.Storage.Models.Entities;

namespace MoneyTracker.Logic.Service.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateUpdateRequest, Category>();
    }
}