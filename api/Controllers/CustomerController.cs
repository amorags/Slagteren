using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[ApiController]

public class CustomerController: ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly CustomerService _customerService;

    public CustomerController(ILogger<ProductController> logger, CustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpGet]
    [Route("/api/customers")]
    public ResponseDto Get()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _customerService.GetCustomerFeed()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/customer")]
    public ResponseDto Post([FromBody]CreateCustomerRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a customer",
            ResponseData = _customerService.CreateCustomer(dto.FirstName, dto.LastName, dto.Email, dto.address, dto.Zip,
                dto.City, dto.Country, dto.Phone)
        };
    }
}