using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Repository.Inteface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public List<OrderDto> getMyOrders(string userId)
        {
            var allOrders = this._orderRepository.getAllOrders();
            var loggedUser = this._userRepository.Get(userId);

            var userOrders = allOrders.Where(z => z.UserId.Equals(loggedUser.Id)).ToList();

            double totalPrice = 0;

            List<OrderDto> ordersList = new List<OrderDto>();

            foreach (var orders in userOrders)
            {
                OrderDto dto = new OrderDto();
                dto.Orders = orders;
                totalPrice = 0;
                foreach (var books in orders.Books)
                {
                    totalPrice += books.Quantity * books.Book.Price;
                }
                dto.TotalPrice = totalPrice;
                ordersList.Add(dto);
            }

            return ordersList;
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.getOrderDetails(model);
        }
    }
}
