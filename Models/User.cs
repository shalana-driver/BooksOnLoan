using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksOnLoan.Models
{
    public class User
    {
        [Key]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }

        public List<Transaction>? Transactions { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction? Transaction { get; set; }
    }
}