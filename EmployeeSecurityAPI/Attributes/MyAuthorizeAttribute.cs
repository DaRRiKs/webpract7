using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeSecurityAPI.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter, IOrderedFilter
{
    private readonly string[] _roles;

    public MyAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public int Order => 0;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!_roles.Any(context.HttpContext.User.IsInRole))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}
