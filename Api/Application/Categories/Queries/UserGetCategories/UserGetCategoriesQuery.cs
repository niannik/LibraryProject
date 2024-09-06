using Application.Common;
using MediatR;

namespace Application.Categories.Queries.UserGetCategories;

public class UserGetCategoriesQuery : IRequest<Result<UserGetCategoriesResponse>>
{
}
