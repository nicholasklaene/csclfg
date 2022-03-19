using api.Models;
using api.Response;
using AutoMapper;

namespace api.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetCategoryResponse>();
    }
}