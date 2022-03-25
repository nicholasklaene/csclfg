using Api.Data;
using Api.Queries;
using Api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Handlers.Query;

public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, GetAllApplicationsResponse>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;

    public GetAllApplicationsQueryHandler(ApplicationDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public Task<GetAllApplicationsResponse> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = _db.Applications.AsNoTracking()
            .Include(a => a.Categories)
            .ThenInclude(c => c.CategoryHasSuggestedTags)
            .ToList()
            .Select(a =>
            {
                var application = _mapper.Map<GetAllApplicationsResponseApplication>(a);
                application.Categories = a.Categories.Select(c =>
                {
                    var category = _mapper.Map<GetAllApplicationsResponseApplicationCategory>(c);
                    category.Tags = c.CategoryHasSuggestedTags.Select(c => c.TagLabel).ToList();
                    return category;
                }).ToList();
                return application;
            })
            .ToList();
        var response = new GetAllApplicationsResponse() {Applications = applications};
        return Task.FromResult(response);
    }
}
