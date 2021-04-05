using HomeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class ServiceProviderController : Controller
    {
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
            List<Booking> bookingrequest = bookinglist.Where(b => b.ServiceProviderId == usr.Usid && b.Servicestatus == false && b.Bookingstatus == false).ToList();
            TempData["ServiceBookingRequests"] = bookingrequest.Count();
            return View();
        }
    }
}
