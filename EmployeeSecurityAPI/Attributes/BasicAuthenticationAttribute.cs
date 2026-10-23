using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using EmployeeSecurityAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeSecurityAPI.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter, IOrderedFilter
{
    public int Order => -1000;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var header = context.HttpContext.Request.Headers.Authorization.ToString();

        if (!AuthenticationHeaderValue.TryParse(header, out var authorization) ||
            !string.Equals(authorization.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(authorization.Parameter))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string credentials;

        try
        {
            credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Parameter));
        }
        catch (FormatException)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var separator = credentials.IndexOf(':');

        if (separator <= 0)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userName = credentials[..separator];
        var password = credentials[(separator + 1)..];
        var user = context.HttpContext.RequestServices.GetRequiredService<UserService>()
            .ValidateCredentials(userName, password);

        if (user is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var identity = new ClaimsIdentity(claims, "Basic");
        context.HttpContext.User = new ClaimsPrincipal(identity);
    }
}
