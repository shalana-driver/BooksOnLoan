using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksOnLoan.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksOnLoan.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<Book>().HasData(
            GetBooks()
        );
        modelBuilder.Entity<Transaction>().HasData(
            GetTransactions()
        );
    }
    public static List<Book> GetBooks() {
        List<Book> books = new List<Book>() {
            new Book() {    
                BookId=1,
                Author="Andrew Chevallier",
                Title="Encyclopedia of Herbal Medicine: 550 Herbs and Remedies for Common Ailments",
                Year=2016,
                Quantity=1,
            },
            new Book() {    
                BookId=2,
                Author="Michael T. Murray M.D. and Joseph Pizzorno",
                Title="The Encyclopedia of Natural Medicine Third Edition",
                Year=2012,
                Quantity=3,
            },
            new Book() {    
                BookId=3,
                Author="Thomas Easley and Steven Horne",
                Title="The Modern Herbal Dispensatory: A Medicine-Making Guide",
                Year=2016,
                Quantity=1,
            },
            new Book() {    
                BookId=4,
                Author="Cat Ellis",
                Title="Prepper's Natural Medicine: Life-Saving Herbs, Essential Oils and Natural Remedies for When There is No Doctor",
                Year=2015,
                Quantity=2,
            },
            new Book() {    
                BookId=5,
                Author="Rosemary Gladstar",
                Title="Rosemary Gladstar's Medicinal Herbs: A Beginner's Guide: 33 Healing Herbs to Know, Grow, and Use",
                Year=2012,
                Quantity=1,
            },
            
           
        };

        return books;
    }
    public static List<Transaction> GetTransactions() {
    List<Transaction> transactions = new List<Transaction>() {
        new Transaction {
            TransactionId = 1,
            BookId = 2,
            UserName = "mm@mm.mm",
            LoanDate = DateTime.Parse("2024-01-19"),
            ReturnDate = DateTime.Parse("2024-01-31"),
            DueDate = DateTime.Parse("2024-02-19"),
            Returned = true
        },
        new Transaction {
            TransactionId = 2,
            BookId = 1,
            UserName = "mm@mm.mm",
            LoanDate = DateTime.Parse("2024-01-29"),
            DueDate = DateTime.Parse("2024-02-29"),
            Returned = false
        },
        new Transaction {
            TransactionId = 3,
            BookId = 4,
            UserName = "mm@mm.mm",
            LoanDate = DateTime.Parse("2024-02-20"),
            ReturnDate = DateTime.Parse("2024-03-25"),
            DueDate = DateTime.Parse("2024-03-20"),
            Returned = true
        },
        new Transaction {
            TransactionId = 4,
            BookId = 5,
            UserName = "mm@mm.mm",
            LoanDate = DateTime.Parse("2024-03-20"),
            DueDate = DateTime.Parse("2024-04-20"),
            Returned = false
        },
        new Transaction {
            TransactionId = 5,
            BookId = 3,
            UserName = "mm@mm.mm",
            LoanDate = DateTime.Parse("2024-03-20"),
            ReturnDate = DateTime.Parse("2024-03-29"),
            DueDate = DateTime.Parse("2024-04-20"),
            Returned = true
        },
    };

    return transactions;
    }

    }
    
}