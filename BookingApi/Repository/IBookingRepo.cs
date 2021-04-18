using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Repository
{
    public interface IBookingRepo
    {
        IEnumerable<Booking> GetBookings();
        Task<Booking> PutBooking(int id, Booking item);
        Booking GetById(int id);    
        Task<Booking> PostBooking(Booking item);
    }
}
