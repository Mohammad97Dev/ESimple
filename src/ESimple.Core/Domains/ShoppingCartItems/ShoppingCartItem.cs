using Abp.Domain.Entities.Auditing;
using ESimple.Domains.Products;
using ESimple.Domains.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.ShoppingCartItems
{
    public class ShoppingCartItem : FullAuditedEntity<int>
    {
        public int ShoppingCartId { get; set; }
        [ForeignKey(nameof(ShoppingCartId))]
        public ShoppingCart ShoppingCart { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Store the price at the time of adding to cart

    }
}
