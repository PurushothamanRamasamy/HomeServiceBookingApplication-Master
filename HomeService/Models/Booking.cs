using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models
{
    public class Booking
    {
        public int Bookingid { get; set; }
        public string CustomerId { get; set; }
        public string ServiceProviderId { get; set; }
        public DateTime? Servicedate { get; set; }
        [Required(ErrorMessage = "Number working Hours cannot be empty")]
        [Display(Name = "Expected number working Hours")]
        public int? Starttime { get; set; }
        public int? Endtime { get; set; }
        public int? Estimatedcost { get; set; }
        public bool? Bookingstatus { get; set; }
        public bool? Servicestatus { get; set; }
        public int? Rating { get; set; }
       
    }
}
