﻿using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service;
using service.Services;

namespace api.Controllers;

[ApiController]
public class  AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly JwtService _jwtService;
    private readonly MailService _mailService;
    

    public AccountController(AccountService service, JwtService jwtService, MailService mailService)
    {
        _accountService = service;
        _jwtService = jwtService;
        _mailService = mailService;
    }

    [HttpPost]
    [Route("/api/account/login")]
    public ResponseDto Login([FromBody] LoginDto dto)
    {
        var user = _accountService.Authenticate(dto.Email, dto.Password);
        
        var token = _jwtService.createToken(user);
        
        return new ResponseDto
        {
            MessageToClient = "Successfully authenticated",
            ResponseData = new {token}
        };
        
    }

    [HttpPost]
    [Route("/api/account/register")]
    [ServiceFilter(typeof(ValidateModel))]
    public ResponseDto Register([FromBody] RegisterUserDto dto)
    {
        // Check if the email is already in use
        if (_accountService.IsEmailInUse(dto.Email))
        {
            HttpContext.Response.StatusCode = 400; // Set the status code to 400 for conflict
            return new ResponseDto
            {
                MessageToClient = "Email is already in use.",
            };
        }

        // If email is not in use, proceed with user registration
        var user = _accountService.Register(dto.FirstName, dto.LastName, dto.Email, dto.Address, dto.Zip, dto.City, dto.Country, dto.Phone, dto.Password);
        
        return new ResponseDto
            
        {
            MessageToClient = "Successfully registered"
        };
    }

    [HttpGet]
    [Route("/api/account/whoami")]
    public ResponseDto WhoAmI()
    {
        throw new NotImplementedException();
    }
}