﻿using api.TransferModels;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResponseDto = api.TransferModels.ResponseDto;

namespace api.Filters;

public class ValidateModel : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;
        string errorMessages = context.ModelState
            .Values
            .SelectMany(i => i.Errors.Select(e => e.ErrorMessage))
            .Aggregate((i, j) => i + "," + j);
        context.Result = new JsonResult(new ResponseDto
        {
            MessageToClient = errorMessages
        })
        {
            StatusCode = 400
        };
    }
}