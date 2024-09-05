using Application.Authors.Commands.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Commands.AdminDeleteAuthor;

public class AdminDeleteAuthorHandler : IRequestHandler<AdminDeleteAuthorCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminDeleteAuthorHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminDeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (author == null)
            return AuthorsErrors.AuthorNotFound;

        _dbContext.Authors.Remove(author);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
