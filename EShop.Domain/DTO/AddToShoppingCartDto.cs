using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Book SelectedBook { get; set; }
        public Guid SelectedBookId { get; set; }
        public int Quantity { get; set; }
    }
}
