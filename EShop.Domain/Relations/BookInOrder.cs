using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.Relations
{
    public class BookInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
