using EShop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class Book : BaseEntity
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string BookImage { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        public virtual ICollection<BookInShoppingCart> BooksInShoppingCart { get; set; }
        public virtual ICollection<BookInOrder> BooksInOrder { get; set; }
    }
}
