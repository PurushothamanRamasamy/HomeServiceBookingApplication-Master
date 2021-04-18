using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models.ViewModel
{
    public class UserModel
    {
        [Required]
        [Remote("IsUserNameExist", "Home",
                ErrorMessage = " Choose another name ")]
        [Display(Name = "Enter your unique username")]
        public string Username { get; set; }
        

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid PinCode")]
        public string Pincode { get; set; }
    }
}
