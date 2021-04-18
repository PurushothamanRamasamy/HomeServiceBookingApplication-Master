using HomeService.Models;
using HomeService.Models.ViewModel;
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
        [HttpPost]
        public async Task<IActionResult> RegisterProvider(ProviderModel userModel)
        {
            User user = new User();
            user.Username = userModel.Username;
            user.Phoneno = TempData.Peek("registermobile").ToString();
            user.Aadhaarno = userModel.Aadhaarno;
            user.Specialization = userModel.Specialization;
            user.Specification = userModel.Specification;
            user.Password = userModel.Password;
            user.ServiceCity = userModel.ServicePincodeOne+","+userModel.ServicePincodeTwo+","+userModel.ServicePincodeThree;
            user.Experience = userModel.Experience;
            user.Costperhour = userModel.Costperhour;
            user.Role = "provider";
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44322/api/users", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            TempData["ProviderDetails"] = JsonConvert.SerializeObject(user);
            return RedirectToAction("DisplayMessage", "home", new { msg = "Succefully Registered", act = "Index", ctrl = "ServiceProvider", isinput = false });
            //return RedirectToAction("Index", "ServiceProvider",new { usr=user});
        }
        public IActionResult RegisterUser()
        {
            
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserModel userModel)
        {

            User user = new User();
            user.Username = userModel.Username;
            user.Phoneno = TempData.Peek("registermobile").ToString();
            user.EmailId = userModel.EmailId;
            user.Address = userModel.Address+","+userModel.Pincode;
            user.Password = userModel.Password;
            user.Role = "user";
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44322/api/users", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            TempData["UserDetails"] = JsonConvert.SerializeObject(user);
            return RedirectToAction("Index", "Users");
        }
    }
}
