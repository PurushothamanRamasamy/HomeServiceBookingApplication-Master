using HomeService.Models;
using Microsoft.AspNetCore.Http;
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
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index(string role)
        {
            if (HttpContext.Session.GetString("IsAdminLoggedin") == null)
            {
                return RedirectToAction("Login", "home");
            }
            Role getrole = new Role();
            getrole.role = role;
            List<User> UserList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            UserList = UserList.Where(usr => usr.IsNewProvider == true && usr.Role == "provider").Select(s => s).ToList();
            TempData["ServiceProviderRequests"] = UserList.Count();
            return View(getrole);
        }
        
        public IActionResult AddSpecilization()
        {
            if (HttpContext.Session.GetString("IsAdminLoggedin") == null)
            {
                return RedirectToAction("Login", "home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSpecilization(Specialization spec)
        {
            string Baseurl = "https://localhost:44350/";
            Specialization Obj = new Specialization();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                StringContent content = new StringContent(JsonConvert.SerializeObject(spec), Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync("/api/Specializations", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Obj = JsonConvert.DeserializeObject<Specialization>(apiResponse);

                }

            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddSpecification()
        {
            if (HttpContext.Session.GetString("IsAdminLoggedin") == null)
            {
                return RedirectToAction("Login", "home");
            }
            List<SelectListItem> DropDownList = new List<SelectListItem>();
            List<Specialization> SpecializationList = new List<Specialization>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44350/api/Specializations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SpecializationList = JsonConvert.DeserializeObject<List<Specialization>>(apiResponse);
                }
            }
            foreach (var item in SpecializationList)
            {
                DropDownList.Add(new SelectListItem() { Text = item.Name, Value = item.Name });
            }
            ViewBag.specialization = DropDownList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSpecification(Specification spec)
        {
            string Baseurl = "https://localhost:44306/"; 

            Specification Obj = new Specification();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                StringContent content = new StringContent(JsonConvert.SerializeObject(spec), Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync("api/Specifications", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Obj = JsonConvert.DeserializeObject<Specification>(apiResponse);

                }

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ServiceProviderRequests()
        {
            if (HttpContext.Session.GetString("IsAdminLoggedin") == null)
            {
                return RedirectToAction("Login", "home");
            }
            List<User> UserList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            UserList = UserList.Where(user => user.IsNewProvider == true && user.Role.ToLower() == "provider").Select(user => user).ToList();
            return View(UserList);
        }
        public async Task<IActionResult> Accept(string id)
        {
            if (HttpContext.Session.GetString("IsAdminLoggedin") == null)
            {
                return RedirectToAction("Login", "home");
            }
            User usr = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usr = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            usr.IsNewProvider = false;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usr), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44322/api/Users/" + id,content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usr = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }

            return RedirectToAction("ServiceProviderRequests");

        }
        
    }
}

