using api.Commands;
using api.Models;
using api.Response;
using AutoMapper;

namespace api.Mappings;

public class ApplicationProfile :  Profile
{
    public ApplicationProfile()
    {
        CreateMap<CreateApplicationCommand, Application>();
        CreateMap<Application, CreateApplicationResponse>();
        CreateMap<Application, GetApplicationResponse>();
    }
}
