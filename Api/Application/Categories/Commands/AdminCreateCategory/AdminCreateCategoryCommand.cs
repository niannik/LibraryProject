using Application.Common;
using MediatR;

namespace Application.Categories.Commands.AdminCreateCategory;

public class AdminCreateCategoryCommand : IRequest<Result>
{
    public required string Title { get; set; }
}
