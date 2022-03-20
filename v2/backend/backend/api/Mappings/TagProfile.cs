using api.Models;
using api.Request;
using AutoMapper;

namespace api.Mappings;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<CreateTagRequest, Tag>();
    }
}
