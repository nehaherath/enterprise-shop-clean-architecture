using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseShop.Domain;

namespace EnterpriseShop.Application.Products
{
    public record AddProductCommand(string Name, string Description, decimal Price, int StockQuantity) : IRequest<Guid>;

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        // 💡 DbContext එක වෙනුවට අපි දැන් ඉන්ජෙක්ට් කරන්නේ Repository Interface එක!
        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CreatedAt = DateTime.UtcNow
            };

            return await _productRepository.AddAsync(product);
        }
    }
}