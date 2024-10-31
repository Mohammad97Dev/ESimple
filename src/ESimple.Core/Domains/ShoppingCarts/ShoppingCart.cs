using Abp.Domain.Entities.Auditing;
using ESimple.Authorization.Users;
using ESimple.Domains.ShoppingCartItems;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.ShoppingCarts
{
    public class ShoppingCart : FullAuditedEntity<int>
    {
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    }
}
