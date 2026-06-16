using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnterpriseShop.Application.Products;

namespace EnterpriseShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        private async Task<string?> SaveImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string name, [FromForm] string description, [FromForm] decimal price, [FromForm] int stockQuantity, IFormFile? image)
        {
            var imagePath = await SaveImageAsync(image);

            var command = new AddProductCommand(name, description, price, stockQuantity, imagePath);
            var productId = await _mediator.Send(command);

            return Ok(new { Id = productId, Message = "Product with image added successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return Ok(new { Message = "Product deleted successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] string name, [FromForm] string description, [FromForm] decimal price, [FromForm] int stockQuantity, [FromForm] string? currentImagePath, IFormFile? image)
        {
            var imagePath = currentImagePath;

            if (image != null)
            {
                imagePath = await SaveImageAsync(image);
            }

            var command = new UpdateProductCommand(id, name, description, price, stockQuantity, imagePath);
            await _mediator.Send(command);
            return Ok(new { Message = "Product updated successfully!" });
        }
    }
}