using Application.Common;
using MediatR;

namespace Application.Categories.Commands.AdminUpdateCategory;

public class AdminUpdateCategoryCommand : IRequest<Result>
{
    public required int Id { get; set; }
    public required string Title { get; set; }
}
