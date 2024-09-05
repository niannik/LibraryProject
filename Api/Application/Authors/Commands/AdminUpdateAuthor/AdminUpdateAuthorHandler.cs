using Application.Authors.Commands.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Commands.AdminUpdateAuthor;

public class AdminUpdateAuthorHandler : IRequestHandler<AdminUpdateAuthorCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminUpdateAuthorHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminUpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author =await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if(author == null)
            return AuthorsErrors.AuthorNotFound;

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.Email = request.Email;
        author.PhoneNumber = request.PhoneNumber;
        author.Address = request.Address;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.Ok();

    }
}
