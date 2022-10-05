using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Interface
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetDetailsForBook(Guid? id);
        void CreateNewBook(Book b);
        void UpdeteExistingBook(Book b);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteBook(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
