﻿using EShop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<BookInShoppingCart> Books { get; set; }

        public double TotalPrice { get; set; }
    }
}
