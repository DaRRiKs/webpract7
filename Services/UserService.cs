using EmployeeSecurityAPI.Models;

namespace EmployeeSecurityAPI.Services;

public class UserService
{
    private readonly User[] _users =
    [
        new() { Id = 1, UserName = "AdminUser", Password = "Admin123", Email = "admin@example.com", Roles = ["Admin"] },
        new() { Id = 2, UserName = "SuperadminUser", Password = "Super123", Email = "superadmin@example.com", Roles = ["Superadmin"] },
        new() { Id = 3, UserName = "user", Password = "user123", Email = "user@example.com", Roles = ["Admin", "Superadmin"] }
    ];

    public User? ValidateCredentials(string userName, string password)
    {
        return _users.FirstOrDefault(user =>
            string.Equals(user.UserName, userName, StringComparison.Ordinal) &&
            string.Equals(user.Password, password, StringComparison.Ordinal));
    }
}
