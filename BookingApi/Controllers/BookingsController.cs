using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingApi.Models;
using BookingApi.Repository;

namespace BookingApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepo _context;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingsController));

        public BookingsController(IBookingRepo context)
        {
            _context = context;
        }

        // GET: api/Specializations
        [HttpGet]
        public IEnumerable<Booking> GetGetBookings()
        {
            _log4net.Info("get Booking is initialized");
            return _context.GetBookings();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            _log4net.Info("Booking table with "+id+"get edited");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editedUser = await _context.PutBooking(id, booking);

            return Ok(editedUser);
        }



        // POST: api/Specializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking specialization)
        {
            _log4net.Info("Booking table with " + specialization + "get aded");
            // _log4net.Info("Specilization post method is initialized");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedSpecilization = await _context.PostBooking(specialization);

            return Ok(addedSpecilization);
        }

        
    }
}
