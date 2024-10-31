using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ESimple.Authorization.Roles;
using ESimple.Authorization.Users;
using ESimple.MultiTenancy;
using ESimple.Domains.Products;
using ESimple.Domains.Orders;
using ESimple.Domains.OrderItems;
using ESimple.Domains.ShoppingCarts;
using ESimple.Domains.ShoppingCartItems;

namespace ESimple.EntityFrameworkCore
{
    public class ESimpleDbContext : AbpZeroDbContext<Tenant, Role, User, ESimpleDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ESimpleDbContext(DbContextOptions<ESimpleDbContext> options)
            : base(options)
        {
        }
    }
}
