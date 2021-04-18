using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models
{
    public class VerifyMobile
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[6-9]{1}[0-9]{9}$", ErrorMessage = "Invalid mobile number")]

        public string Phoneno { get; set; }
    }
}
