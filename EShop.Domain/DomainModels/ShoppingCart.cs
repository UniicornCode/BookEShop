using EShop.Domain.Identity;
using EShop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public virtual ICollection<BookInShoppingCart> BooksInShoppingCart { get; set; }
    }
}
