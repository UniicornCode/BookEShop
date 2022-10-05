using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.DTO
{
    public class OrderDto
    {
        public Order Orders { get; set; }
        public double TotalPrice { get; set; }
    }
}
