using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseShop.Domain
{
    public interface IProductRepository
    {
        Task<Guid> AddAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id); 
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}