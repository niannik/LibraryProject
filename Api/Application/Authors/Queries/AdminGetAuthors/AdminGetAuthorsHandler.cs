using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Queries.AdminGetAuthors;

public class AdminGetAuthorsHandler : IRequestHandler<AdminGetAuthorsQuery, Result<AdminGetAuthorsResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminGetAuthorsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<AdminGetAuthorsResponse>> Handle(AdminGetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var response = new AdminGetAuthorsResponse
        {
            Items = await _dbContext.Authors.Select(x => new AdminAuthorItem
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
            }).ToListAsync(cancellationToken)
        };

        return response;
    }
}
