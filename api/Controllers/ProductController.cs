using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service;
using service.Services;

namespace api.Controllers;

[ApiController]

public class ProductController : ControllerBase
{
  
    //Logger, not sure we'll use it but let's let i bee for now
    
    private readonly ILogger<ProductController> _logger;
    private readonly ProductService _productService;

    public ProductController(ILogger<ProductController> logger, ProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    //Endpoint (Dummy)

    [HttpGet]
    [Route("/api/products")]
    public ResponseDto Get()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _productService.GetProductFeed()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/product")]
    public ResponseDto Post([FromBody]CreateProductRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a product",
            ResponseData = _productService.CreateProduct(dto.ProductNumber, dto.ProductName, dto.PricePrKilo,
                dto.ProductType, dto.CountryOfBirth, dto.ProductionCountry, dto.Description, dto.ImgUrl, dto.MinExpDate)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/product/{productId}")]
    public ResponseDto Put([FromRoute] int productId, [FromBody] UpdateProductRequestDto dto)
    {
        HttpContext.Response.StatusCode = 201;
        return new ResponseDto()
        {
            MessageToClient = "Successfully Update a product",
            ResponseData = _productService.UpdateProduct(dto.ProductId, dto.ProductNumber, dto.ProductName, dto.PricePrKilo,
                dto.ProductType, dto.CountryOfBirth, dto.ProductionCountry, dto.Description, dto.ImgUrl, dto.MinExpDate)
        };
    }

    [HttpDelete]
    [Route("/api/product/{productId}")]
    public ResponseDto Delete([FromRoute] int productId)
    {
        _productService.DeleteProduct(productId);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted"
        };

    }

}
