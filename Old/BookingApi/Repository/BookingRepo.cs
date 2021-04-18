﻿using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Repository
{
    public class BookingRepo:IBookingRepo
    {
        private readonly ServiceBookingContext _context;


        public BookingRepo()
        {

        }
        public BookingRepo(ServiceBookingContext context)
        {
            _context = context;
        }
        public IEnumerable<Booking> GetBookings()
        {
            return _context.Bookings.ToList();
        }

        public async Task<Booking> PutBooking(int id, Booking item)
        {
            Booking Sp = await _context.Bookings.FindAsync(id);
            Sp.Bookingid = item.Bookingid;
            Sp.CustomerId = item.CustomerId;
            Sp.ServiceProviderId = item.ServiceProviderId;
            Sp.Servicedate = item.Servicedate;
            Sp.Starttime = item.Starttime;
            Sp.Endtime = item.Endtime;
            Sp.Estimatedcost = item.Estimatedcost;
            Sp.Bookingstatus = item.Bookingstatus;
            Sp.Servicestatus = item.Servicestatus;
            Sp.Rating = item.Rating;
            _context.SaveChanges();
            return Sp;
        }

        public async Task<Booking> PostBooking(Booking item)
        {
            Booking book =null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                book=new Booking {CustomerId = item.CustomerId,ServiceProviderId=item.ServiceProviderId,Servicedate=item.Servicedate,Starttime=item.Starttime,Endtime=item.Endtime,Estimatedcost=item.Estimatedcost,Bookingstatus=item.Bookingstatus,Servicestatus=item.Servicestatus,Rating=item.Rating};
                await _context.Bookings.AddAsync(book);
                await _context.SaveChangesAsync();
            }
            return book;
        }
        public Booking GetById(int id)
        {
            Booking bk = _context.Bookings.FirstOrDefault(book => book.Bookingid == id);
            return bk;
        }





        /*  public async Task<SpecializationTable> RemoveSpecialization(string id)
 {
     SpecializationTable sp = await _context.Specializations.FindAsync(id);
     if (sp == null)
     {
         throw new NullReferenceException();
     }
     else
     {
         _context.Specializations.Remove(sp);
         await _context.SaveChangesAsync();
     }
     return sp;
 }*/
    }
}
