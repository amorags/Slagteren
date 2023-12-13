using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[ApiController]

public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;

    public UserController(ILogger<UserController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    [Route("/api/users")]
    public ResponseDto Get()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _userService.GetUserFeed()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/user")]
    public ResponseDto Post([FromBody]CreateUserRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a new User",
            ResponseData = _userService.CreateUser(dto.FirstName, dto.LastName, dto.Email, dto.address, dto.Zip,
                dto.City, dto.Country, dto.Phone)
        };
    }
}