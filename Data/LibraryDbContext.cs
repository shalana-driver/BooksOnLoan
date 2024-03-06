using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksOnLoan.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksOnLoan.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options) {}


        public DbSet<Book>? Books { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=library.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().ToTable("Book");
            builder.Entity<Transaction>().ToTable("Transaction");

            builder.Seed();
        }
    }
}