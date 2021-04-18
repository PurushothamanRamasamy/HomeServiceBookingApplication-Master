using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models.ViewModel
{
    public class ShowBookingRequests
    {
        public int BookingId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? ServiceDate { get; set; }
        public int ServiceHours { get; set; }
        public int? ServiceCost { get; set; }
    }
}
