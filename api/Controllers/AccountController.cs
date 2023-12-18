using api.Filters;
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
    

    public AccountController(AccountService service)
    {
        _accountService = _accountService;
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
            ResponseData = token
        };

        
    }

    [HttpPost]
    [Route("/api/account/register")]
    public ResponseDto Register([FromBody] RegisterUserDto dto)
    {
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