using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relations;
using EShop.Repository.Inteface;
using EShop.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookInShoppingCart> _bookInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<BookService> _logger;

        public BookService (IRepository<Book> bookRepository, 
            IRepository<BookInShoppingCart> bookInShoppingCartRepository, 
            IUserRepository userRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _logger = logger;
            _bookInShoppingCartRepository = bookInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserShoppingCart;

            if (item.SelectedBookId != null && userShoppingCard != null)
            {
                var product = this.GetDetailsForBook(item.SelectedBookId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (product != null)
                {
                    BookInShoppingCart itemToAdd = new BookInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Book = product,
                        BookId = product.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    var existing = userShoppingCard.BooksInShoppingCart.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.BookId == itemToAdd.BookId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this._bookInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this._bookInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong!");
            return false;
        }

        public void CreateNewBook(Book b)
        {
            this._bookRepository.Insert(b);
        }

        public void DeleteBook(Guid id)
        {
            var book = this._bookRepository.Get(id);
            this._bookRepository.Delete(book);
        }

        public List<Book> GetAllBooks()
        {
            _logger.LogInformation("We got all books");
            return this._bookRepository.GetAll().ToList();
        }

        public Book GetDetailsForBook(Guid? id)
        {
            return this._bookRepository.Get(id);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var book = this.GetDetailsForBook(id);
            var item = new AddToShoppingCartDto
            {
                SelectedBook = book,
                SelectedBookId = book.Id,
                Quantity = 1
            };
            return item;
        }

        public void UpdeteExistingBook(Book b)
        {
            this._bookRepository.Update(b);
        }
    }
}
