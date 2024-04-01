using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BooksOnLoan.Models
{
    public class ExtendedUser : IdentityUser
    {
        public ExtendedUser() : base() { }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
    }
}