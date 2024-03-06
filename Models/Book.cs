using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksOnLoan.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "ID")]
        public int BookId { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        [Range(0, 2024, ErrorMessage = "Year cannot be later than 2024")]
        public int? Year { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be less than 0")]
        public int? Quantity { get; set; }

        public List<Transaction>? Transactions { get; set; }
    }
}