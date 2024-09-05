using Application.Common;
using MediatR;

namespace Application.Authors.Queries.UserGetAuthors;

public class UserGetAuthorsQuery : IRequest<Result<UserGetAuthorsResponse>>
{
}
