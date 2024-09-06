using Application.Books.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.AdminDeleteBook;

public class AdminDeleteBookHandler : IRequestHandler<AdminDeleteBookCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminDeleteBookHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminDeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (book == null) 
            return BookErrors.BookNotFound;

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
