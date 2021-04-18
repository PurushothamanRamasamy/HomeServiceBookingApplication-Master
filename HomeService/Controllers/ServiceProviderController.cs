using HomeService.Models;
using HomeService.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class ServiceProviderController : Controller
    {
        public static Booking bookingrequest;
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("IsProviderLoggedin") == null)
            {
                return RedirectToAction("Login", "home");
            }
            string strUser = TempData.Peek("ProviderDetails").ToString();
            User usr = JsonConvert.DeserializeObject<User>(strUser);
            List<Booking> bookinglist = new List<Booking>();
            if (usr.IsNewProvider==false&&usr.Role== "provider")
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44349/api/Bookings"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bookinglist = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                    }

                }
            }
            bookingrequest = bookinglist.FirstOrDefault(b => b.ServiceProviderId == usr.Usid && b.Servicestatus == false && b.Bookingstatus == false);
            if (bookingrequest!=null)
            {
                TempData["ServiceBookingRequests"] = 1;
            }
            else
            {
                TempData["ServiceBookingRequests"] = 0;
            }
            
            return View();
        }

        public async Task<IActionResult> BookingRequests()
        {
            List<ShowBookingRequests> bookingRequests = new List<ShowBookingRequests>();

            List<User> Userslist = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users/getusers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Userslist = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }

            }
           
                foreach (User usersl in Userslist)
                {
                    if (bookingrequest.CustomerId==usersl.Usid)
                    {
                        bookingRequests.Add(new ShowBookingRequests {
                            BookingId=bookingrequest.Bookingid,
                            UserName=usersl.Username,
                            PhoneNumber=usersl.Phoneno,
                            Email=usersl.EmailId,
                            Address=usersl.Address,
                            ServiceDate= bookingrequest.Servicedate,
                            ServiceHours= bookingrequest.Starttime,
                            ServiceCost= bookingrequest.Estimatedcost });
                    }
                }
                
           
            return View(bookingRequests);
        }
        public async Task<IActionResult> Accept(int id)
        {
           
            Booking book = new Booking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44349/api/Bookings/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Booking>(apiResponse);
                }
            }
            book.Bookingstatus = true;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44349/api/Bookings/" + id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Booking>(apiResponse);
                }
            }
            return RedirectToAction("DisplayMessage", "home", new { msg = "Booking Request has successfully accepted", act = "Index", ctrl = "ServiceProvider", isinput = false });

            //return RedirectToAction("Index");

        }
    }
}
