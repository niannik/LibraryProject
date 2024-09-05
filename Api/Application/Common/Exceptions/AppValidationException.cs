using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class AppValidationException : Exception
{
    public AppValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = Array.Empty<Error>();
    }

    public AppValidationException(IEnumerable<ValidationFailure> failures)
    {
        Errors = failures
            .Select(f => new Error(f.ErrorMessage, f.ErrorCode))
            .ToArray();
    }
    public AppValidationException(params Error[] errors)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

}

