using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relations;
using EShop.Repository.Inteface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _emailRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService (IRepository<ShoppingCart> shoppingCartRepository, 
            IUserRepository userRepository, IRepository<Order> orderRepository,
            IRepository<BookInOrder> bookInOrderRepository, IRepository<EmailMessage> emailRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _emailRepository = emailRepository;
            _bookInOrderRepository = bookInOrderRepository;
            _userRepository = userRepository;
        }

        public bool DeleteBookFromShoppingCart(string userId, Guid bookId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            var itemToDelete = userShoppingCart.BooksInShoppingCart.Where(z => z.BookId.Equals(bookId)).FirstOrDefault();

            var deletedItem = userShoppingCart.BooksInShoppingCart.Remove(itemToDelete);

            _shoppingCartRepository.Update(userShoppingCart);

            return deletedItem;
        }

        public bool EditBookInShoppingCart(string userId, Guid bookId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            var itemToDelete = userShoppingCart.BooksInShoppingCart.Where(z => z.BookId.Equals(bookId)).FirstOrDefault();

            var deletedItem = userShoppingCart.BooksInShoppingCart.Remove(itemToDelete);

            _shoppingCartRepository.Update(userShoppingCart);

            return deletedItem;
        }

        public ShoppingCartDto GetShoppingCartInfo(string userId)
        {
            var user = _userRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;

            var allBooksInCart = userShoppingCart.BooksInShoppingCart;

            double totalPrice = 0;

            foreach(var books in allBooksInCart)
            {
                totalPrice += books.Quantity * books.Book.Price;
            }

            return new ShoppingCartDto
            {
                Books = allBooksInCart.ToList(),
                TotalPrice = totalPrice
            };
        }

        public bool Order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserShoppingCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "succesfully created order";
                mail.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<BookInOrder> productInOrders = new List<BookInOrder>();

                var result = userCard.BooksInShoppingCart.Select(z => new BookInOrder
                {
                    Id = Guid.NewGuid(),
                    BookId = z.BookId,
                    Book = z.Book,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Book.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Book.BookName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Book.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                mail.Content = sb.ToString();


                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this._bookInOrderRepository.Insert(item);
                }

                loggedInUser.UserShoppingCart.BooksInShoppingCart.Clear();

                this._userRepository.Update(loggedInUser);
                this._emailRepository.Insert(mail);

                return true;
            }

            return false;
        }
    }
}
