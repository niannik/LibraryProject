using Application.Common;

namespace Application.Authors.Commands.Common;

public class AuthorsErrors
{
    public static Error AuthorNotFound = new("Author Not found", "Author");
}
