using Application.Common;

namespace Api.Models;

public record BadRequestBody
{
    public BadRequestBody()
    {
        Errors = [];
    }

    public BadRequestBody(Error? error)
    {
        Errors = error is null ? Array.Empty<Error>() : new[] { error.Value };
    }

    public BadRequestBody(Error[] errors)
    {
        Errors = errors;
    }

    public Error[] Errors { get; init; }
    public int Status { get; } = StatusCodes.Status400BadRequest;
    public string Type { get; } = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
}