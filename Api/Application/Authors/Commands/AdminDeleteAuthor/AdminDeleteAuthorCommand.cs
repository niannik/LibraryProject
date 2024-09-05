using Application.Common;
using MediatR;

namespace Application.Authors.Commands.AdminDeleteAuthor;

public class AdminDeleteAuthorCommand : IRequest<Result>
{
    public required int Id { get; set; }
}
