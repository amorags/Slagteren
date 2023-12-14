using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service.Services;


namespace api.Controllers;

[ApiController]

public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;
    private readonly TokenRepository _tokenRepository;
    

    public UserController(ILogger<UserController> logger, UserService userService, TokenRepository tokenRepository)
    {
        _logger = logger;
        _userService = userService;
        _tokenRepository = tokenRepository;
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

    [HttpPut]
    [ValidateModel]
    [Route("/api/user/{userId}")]
    public ResponseDto Put([FromRoute] int userId, [FromBody] UpdateUserRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully Update the User",
            ResponseData = _userService.UpdateUser(dto.UserId, dto.FirstName, dto.LastName, dto.Email, dto.address,
                dto.Zip, dto.City, dto.Country, dto.Phone)
        };
    }

    [HttpDelete]
    [Route("/api/user/{userId}")]
    public ResponseDto Delete([FromRoute] int userId)
    {
        _userService.DeleteUser(userId);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted"
        };
    }

    [HttpPost]
    [Route("/login")]
    public IActionResult login([FromBody] UserLogin userToBeLoggedIn)
    {
        User userToBeAutheticatedd = _userService.login(userToBeLoggedIn);
        if (userToBeLoggedIn == null)
        {
            throw new Exception("Login failed, user could not be authenticated");
        }

        var token = JwtService.createToken(userToBeAutheticatedd);

        return Ok(token);
    }
}