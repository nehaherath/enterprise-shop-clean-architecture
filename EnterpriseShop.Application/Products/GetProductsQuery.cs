using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseShop.Domain;

namespace EnterpriseShop.Application.Products
{
    public record GetProductsQuery() : IRequest<IEnumerable<Product>>;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}