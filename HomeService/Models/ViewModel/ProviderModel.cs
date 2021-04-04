using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models.ViewModel
{
    public class ProviderModel
    {
        [Required]
        [Remote("IsUserNameExist", "Home",
                ErrorMessage = "Choose another name")]
        public string Username { get; set; }
        

        
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
        public string Specialization { get; set; }
        [Required]
        public string Specification { get; set; }
        
        [Required(ErrorMessage ="You should Provide atleast one service  PinCode")]
        [Display(Name ="Service Pincode")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid PinCode")]
        public string ServicePincodeOne { get; set; }
        [Display(Name = "Service Pincode two")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid PinCode")]
        public string ServicePincodeTwo{ get; set; }
        [Display(Name = "Service Pincode three")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid PinCode")]
        public string ServicePincodeThree { get; set; }
        [Required]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Invalid Aadhaar number")]
        [Remote("IsUserAadhaarExist", "Home",
                ErrorMessage = "Aadhaar number already exists")]
        public string Aadhaarno { get; set; }
        [Required]
        [Display(Name ="Enter your experience in years")]
        public int? Experience { get; set; }
        [Required]
        [Display(Name = "Enter your service cost per hour")]
        public int? Costperhour { get; set; }
    }
}
