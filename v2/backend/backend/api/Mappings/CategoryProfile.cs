using api.Commands;
using api.Models;
using api.Response;
using AutoMapper;

namespace api.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetCategoryResponse>();
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CreateCategoryResponse>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<Category, UpdateCategoryResponse>();
    }
}
