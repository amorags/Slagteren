using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]

public class ProductController : ControllerBase
{
  
    //Logger, not sure we'll use it but let's let i bee for now
    
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    //Endpoint (Dummy)

    [HttpGet]
    [Route("/product")]

    public object Get()
    {
        return "Oksekød";
    }


}
