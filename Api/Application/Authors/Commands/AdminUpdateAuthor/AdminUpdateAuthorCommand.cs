using Application.Common;
using MediatR;

namespace Application.Authors.Commands.AdminUpdateAuthor;

public class AdminUpdateAuthorCommand : IRequest<Result>
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
