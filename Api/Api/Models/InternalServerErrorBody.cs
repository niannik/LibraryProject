using Application.Common;

namespace Api.Models;

public record InternalServerErrorBody
{
    public InternalServerErrorBody()
    {
        Errors = Array.Empty<Error>();
    }

    public InternalServerErrorBody(Error? error)
    {
        Errors = error is null ? Array.Empty<Error>() : new[] { error.Value };
    }

    public InternalServerErrorBody(Error[] errors)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }
    public int Status { get; } = StatusCodes.Status500InternalServerError;
    public string Type { get; } = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
}
