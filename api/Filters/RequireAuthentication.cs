﻿using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Filters;

public class RequireAuthentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.GetSessionData() == null) throw new AuthenticationException();
    } 
}