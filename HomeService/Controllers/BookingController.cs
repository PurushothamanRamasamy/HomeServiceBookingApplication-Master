using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult BookService(string id, int cost)
        {

            TempData["msg"] = id;
            TempData["msg1"] = cost;
            Booking booking = new Booking();
            booking.ServiceProviderId = id;
            booking.Estimatedcost = cost;
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> BookService(Booking spec)
        {
            string strUser = TempData.Peek("UserDetails").ToString();
            User usr = JsonConvert.DeserializeObject<User>(strUser);
            spec.CustomerId = usr.Usid;
            Booking Obj = new Booking();
            spec.Estimatedcost = spec.Starttime * spec.Estimatedcost;
            
            using (var client = new HttpClient())
            {
               

                StringContent content = new StringContent(JsonConvert.SerializeObject(spec), Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync("https://localhost:44349/api/Bookings", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Obj = JsonConvert.DeserializeObject<Booking>(apiResponse);

                }

            }

            return RedirectToAction("DisplayMessage", "home", new { msg = "Succefully Booked Wait For Sometime", act = "Index", ctrl = "Users", isinput = false });

        }

    }
}
