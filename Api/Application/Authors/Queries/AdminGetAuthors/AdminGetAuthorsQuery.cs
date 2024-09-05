using Application.Common;
using MediatR;

namespace Application.Authors.Queries.AdminGetAuthors;

public class AdminGetAuthorsQuery : IRequest<Result<AdminGetAuthorsResponse>>
{
}
