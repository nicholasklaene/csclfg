using Api.Commands;
using Api.Models;
using Api.Response;
using AutoMapper;

namespace Api.Mappings;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Application, GetAllApplicationsResponseApplication>();
        CreateMap<Category, GetAllApplicationsResponseApplicationCategory>();
        CreateMap<Application, GetApplicationByIdResponse>();
        CreateMap<Category, GetApplicationByIdResponseCategory>();
        CreateMap<CreateApplicationCommand, Application>();
        CreateMap<Application, CreateApplicationResponse>();
        CreateMap<UpdateApplicationCommand, Application>();
        CreateMap<Application, UpdateApplicationResponse>();
    }
}
