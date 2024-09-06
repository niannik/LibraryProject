using Application.Common;
using MediatR;

namespace Application.Categories.Commands.AdminDeleteCategory;

public class AdminDeleteCategoryCommand : IRequest<Result>
{
    public required int Id { get; set; }
}
