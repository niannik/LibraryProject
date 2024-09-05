using Application.Common;

namespace Application.Auth.Commands;

public static class AuthErrors
{
    public static Error InvalidAdminLogin = new("UserName or Password is Wrong.", "Invalid_Admin_Login");
}
