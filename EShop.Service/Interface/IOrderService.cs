using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public List<OrderDto> getMyOrders(string userId);
        public Order getOrderDetails(BaseEntity model);
    }
}
