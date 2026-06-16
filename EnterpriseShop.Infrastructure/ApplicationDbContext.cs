using Microsoft.EntityFrameworkCore;
using EnterpriseShop.Domain;

namespace EnterpriseShop.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}