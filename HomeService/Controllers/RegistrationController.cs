using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Registration()
        {
           // @Html.DropDownListFor(model => model.Specialization, new SelectList(ViewBag.specialization, "Value", "Text"), "--- Select ---", htmlAttributes: new { @class = "form-control", @id = "txtrole" })

            List<SelectListItem> DropDownList = new List<SelectListItem>();
            List<Specialization> SpecificationList = new List<Specialization>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44350/api/Specializations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SpecificationList = JsonConvert.DeserializeObject<List<Specialization>>(apiResponse);
                }
            }
            foreach (var item in SpecificationList)
            {
                DropDownList.Add(new SelectListItem() { Text = item.Name, Value = item.Name });
            }
            ViewBag.specialization = DropDownList;
            return View();
        }
        public async Task<IActionResult> RegisterProvider()
        {
            List<SelectListItem> DropDownList = new List<SelectListItem>();
            List<Specialization> SpecificationList = new List<Specialization>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44350/api/Specializations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SpecificationList = JsonConvert.DeserializeObject<List<Specialization>>(apiResponse);
                }
            }
            foreach (var item in SpecificationList)
            {
                DropDownList.Add(new SelectListItem() { Text = item.Name, Value = item.Name });
            }
            ViewBag.specialization = DropDownList;
            return View();
            
        }
        public IActionResult RegisterUser()
        {
            
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(User user)
        {

            User usr = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44322/api/users", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usr = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return RedirectToAction("Index", "home");
        }
    }
}
