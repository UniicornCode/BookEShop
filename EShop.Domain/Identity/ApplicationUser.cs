using EShop.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EShop.Domain.Identity
{
    public enum Genres
    {
        Horror, Thriller, Romance, Science, Fiction, Other
    }
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Genres FavoriteGenre { get; set; }
        public ShoppingCart UserShoppingCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
