using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// A single user account.
    /// </summary>
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        [StringLength(24)]
        public string UserName { get; set; }

        [StringLength(60)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)] //<input type="password">
        [StringLength(150)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        public string Address { get; set; }

        // https://en.wikipedia.org/wiki/Children%27s_Online_Privacy_Protection_Act
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }

    /// <summary>
    /// View Model, look up Panopto @ approx 19min for description
    /// </summary>
    public class RegisterViewModel
    {
        // Account ID auto generated

        [Required]
        [StringLength(60)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))] // we could hard code [Compare("Password")], but that does not have refactoring support
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
    }
}
