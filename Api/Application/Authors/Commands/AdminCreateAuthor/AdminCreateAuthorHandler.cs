using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.AuthorAggregate;
using MediatR;

namespace Application.Authors.Commands.AdminCreateAuthor;
public class AdminCreateAuthorHandler : IRequestHandler<AdminCreateAuthorCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminCreateAuthorHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminCreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
        };

        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
