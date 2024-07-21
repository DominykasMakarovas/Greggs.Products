using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get(int pageStart = 0, int pageSize = 5)
    {
        return Ok(_productService.GetProducts(pageStart, pageSize));
    }

    [HttpGet]
    [Route("euros")]
    public ActionResult<IEnumerable<Product>> GetProductEurope(int pageStart = 0, int pageSize = 5) { 
        return Ok(_productService.GetProductsInEuros(pageStart, pageSize));
    }
}