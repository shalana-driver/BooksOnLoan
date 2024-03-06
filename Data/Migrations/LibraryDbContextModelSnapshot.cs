﻿// <auto-generated />
using System;
using BooksOnLoan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BooksOnLoan.Data.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("BooksOnLoan.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Year")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("BookId");

                    b.ToTable("Book", (string)null);

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "Andrew Chevallier",
                            Quantity = 1,
                            Title = "Encyclopedia of Herbal Medicine: 550 Herbs and Remedies for Common Ailments",
                            Year = 2016
                        },
                        new
                        {
                            BookId = 2,
                            Author = "Michael T. Murray M.D. and Joseph Pizzorno",
                            Quantity = 3,
                            Title = "The Encyclopedia of Natural Medicine Third Edition",
                            Year = 2012
                        },
                        new
                        {
                            BookId = 3,
                            Author = "Thomas Easley and Steven Horne",
                            Quantity = 1,
                            Title = "The Modern Herbal Dispensatory: A Medicine-Making Guide",
                            Year = 2016
                        },
                        new
                        {
                            BookId = 4,
                            Author = "Cat Ellis",
                            Quantity = 2,
                            Title = "Prepper's Natural Medicine: Life-Saving Herbs, Essential Oils and Natural Remedies for When There is No Doctor",
                            Year = 2015
                        },
                        new
                        {
                            BookId = 5,
                            Author = "Rosemary Gladstar",
                            Quantity = 1,
                            Title = "Rosemary Gladstar's Medicinal Herbs: A Beginner's Guide: 33 Healing Herbs to Know, Grow, and Use",
                            Year = 2012
                        });
                });

            modelBuilder.Entity("BooksOnLoan.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LoanDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Returned")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionId");

                    b.HasIndex("BookId");

                    b.ToTable("Transaction", (string)null);

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            BookId = 2,
                            LoanDate = new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnDate = new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Returned = false,
                            UserName = "mm@mm.mm"
                        },
                        new
                        {
                            TransactionId = 2,
                            BookId = 4,
                            LoanDate = new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReturnDate = new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Returned = false,
                            UserName = "mm@mm.mm"
                        });
                });

            modelBuilder.Entity("BooksOnLoan.Models.Transaction", b =>
                {
                    b.HasOne("BooksOnLoan.Models.Book", "Book")
                        .WithMany("Transactions")
                        .HasForeignKey("BookId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BooksOnLoan.Models.Book", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
