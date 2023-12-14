using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service;
using service.Services;

namespace api.Controllers;

[ValidateModel]
public class  AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("/api/account/login")]
    public ResponseDto Login([FromBody] LoginDto dto)
    {
        var customer = _service.Authenticate(dto.Email, dto.Password);
        return new ResponseDto
        {
            MessageToClient = "Successfully authenticated"
        };
    }

    [HttpPost]
    [Route("/api/account/register")]
    public ResponseDto Register([FromBody] RegisterUserDto dto)
    {
        var customer = _service.Register(dto.FirstName, dto.LastName, dto.Email, dto.Address, dto.Zip, dto.City, dto.Country, dto.Phone, dto.Password);
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