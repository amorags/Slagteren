using service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Attributes;

public class AuthorizeAdmin : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenService = (TokenService)context.HttpContext.RequestServices.GetService(typeof(TokenService));
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        try
        {
            // Validate the token and get the admin user
            var user = tokenService.validateTokenAndReturnUser(token);

            // Additional step to confirm the user has an admin role
            if (user.UserRole != "admin" || user.Deleted)
            {
                context.Result = new UnauthorizedResult();
            }
            // Store the admin user in HttpContext so we can retrieve it later on
            context.HttpContext.Items["User"] = user;
        }
        catch (SecurityTokenException)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}