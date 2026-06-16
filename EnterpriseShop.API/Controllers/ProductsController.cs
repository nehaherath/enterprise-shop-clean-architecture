using MediatR;
using Microsoft.AspNetCore.Mvc;
using EnterpriseShop.Application.Products;

namespace EnterpriseShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(new { Id = productId, Message = "Product added successfully to PostgreSQL!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }
    }
}