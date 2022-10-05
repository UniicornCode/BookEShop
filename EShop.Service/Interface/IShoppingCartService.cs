using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartInfo(string userId);
        bool DeleteBookFromShoppingCart(string userId, Guid bookId);
        bool EditBookInShoppingCart(string userId, Guid bookId);
        bool Order(string userId);
    }
}
