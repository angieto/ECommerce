using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class EContext: DbContext
    {
        // base(options) calls the parent class's constructor
        public EContext(DbContextOptions<EContext> options) : base(options) {}
        public DbSet<Customer> Customers {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<Order> Orders {get; set;}
    }
}