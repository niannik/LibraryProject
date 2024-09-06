using Application.Common;
using MediatR;

namespace Application.Categories.Queries.AdminGetCategories;

public class AdminGetCategoriesQuery : IRequest<Result<AdminGetCategoriesResponse>>
{
}
