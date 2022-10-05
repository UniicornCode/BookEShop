using EShop.Domain.Identity;
using EShop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<BookInOrder> Books { get; set; }

    }
}
