using System;
using MediatR;
using EnterpriseShop.Domain;

namespace EnterpriseShop.Application.Products
{
    public record DeleteProductCommand(Guid Id) : IRequest;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
        }
    }
}