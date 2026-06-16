using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EnterpriseShop.Domain;

namespace EnterpriseShop.Application.Products
{
    // 🛠️ මෙන්න මෙතන arguments 6ම ලස්සනට ඇතුළත් කරලා තියෙනවා (ImagePath එකත් එක්කම)
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, int StockQuantity, string? ImagePath) : IRequest;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _repository.GetByIdAsync(request.Id);
            
            if (existingProduct != null)
            {
                existingProduct.Name = request.Name;
                existingProduct.Description = request.Description;
                existingProduct.Price = request.Price;
                existingProduct.StockQuantity = request.StockQuantity;
                
                if (!string.IsNullOrEmpty(request.ImagePath))
                {
                    existingProduct.ImagePath = request.ImagePath;
                }

                await _repository.UpdateAsync(existingProduct);
            }
        }
    }
}