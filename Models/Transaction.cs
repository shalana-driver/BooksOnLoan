using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksOnLoan.Models
{
    public class Transaction
    {
        [Key]
        [Display(Name = "ID")]
        public int TransactionId { get; set; }
        [Display(Name = "Book ID")]
        public int? BookId { get; set; }
        [Display(Name = "User Name")]
        public string? UserName { get; set; }
        [Display(Name = "Loan Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime?  LoanDate { get; set; }
        [Display(Name = "Return Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }
        public bool? Returned { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }
    }
}