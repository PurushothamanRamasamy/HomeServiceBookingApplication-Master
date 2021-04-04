using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class BookingController : Controller
    {
        string Baseurl = "https://localhost:44349/";
        public IActionResult Index(string id)
        {
            return View();
        }

        public ActionResult BookService(string id, int cost)
        {
            
            TempData["msg"] = id;
            TempData["msg1"] = cost;
            Booking booking = new Booking();
            booking.Estimatedcost = cost;
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> BookService(Booking spec)
        {
            string id = (string)TempData.Peek("msg");
            int cost = (int)TempData.Peek("msg1");
            Booking Obj = new Booking();
            spec.CustomerId = id;
            spec.ServiceProviderId = id;
            spec.Estimatedcost = cost * spec.Starttime;
            if (spec.CustomerId == id)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);


                    client.DefaultRequestHeaders.Clear();

                    StringContent content = new StringContent(JsonConvert.SerializeObject(spec), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("/api/Bookings", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Obj = JsonConvert.DeserializeObject<Booking>(apiResponse);

                    }

                }
                return RedirectToAction("Index");
            }
            else
                return View();
        }


    }
}

