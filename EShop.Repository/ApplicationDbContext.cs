using EShop.Domain.DomainModels;
using EShop.Domain.Identity;
using EShop.Domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<BookInShoppingCart> BookInShoppingCarts { get; set; }
        public virtual DbSet<BookInOrder> BookInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Order>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<BookInShoppingCart>()
                .HasKey(z => new { z.BookId, z.ShoppingCartId });

            builder.Entity<BookInOrder>()
                .HasKey(z => new { z.BookId, z.OrderId });

            builder.Entity<BookInShoppingCart>()
                .HasOne(z => z.Book)
                .WithMany(z => z.BooksInShoppingCart)
                .HasForeignKey(z => z.BookId);

            builder.Entity<BookInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.BooksInShoppingCart)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ShoppingCart>()
                .HasOne<ApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserShoppingCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            builder.Entity<BookInOrder>()
                .HasOne(z => z.Book)
                .WithMany(z => z.BooksInOrder)
                .HasForeignKey(z => z.BookId);

            builder.Entity<BookInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.Books)
                .HasForeignKey(z => z.OrderId);
        }
    }
}
