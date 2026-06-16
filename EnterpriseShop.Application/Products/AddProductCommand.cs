using System;
using MediatR;
using EnterpriseShop.Domain;

namespace EnterpriseShop.Application.Products
{
    public record AddProductCommand(string Name, string Description, decimal Price, int StockQuantity, string? ImagePath) : IRequest<Guid>;

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _repository;

        public AddProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
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
                ImagePath = request.ImagePath 
            };

            return await _repository.AddAsync(product);
        }
    }
}