using HomeService.Models;
using HomeService.Models.ViewModel;
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
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index(User lst)
        {
            if (HttpContext.Session.GetString("IsUserLoggedin") == null)
            {
                return RedirectToAction("Login","home");
            }
            
            string strUser = TempData.Peek("UserDetails").ToString();
            lst = JsonConvert.DeserializeObject<User>(strUser);
            
            List<User> SpecificationList = new List<User>();
            List<User> ListProviders = new List<User>();
            List<string> useraddress = lst.Address.Split(",").ToList();
            
            string userpincode = useraddress[useraddress.Count() - 1];
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users/providers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SpecificationList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
           List<User> sortedList = SpecificationList.OrderByDescending(u=>u.Rating).ToList();
            foreach (User item in sortedList)
            {
                if(item.ServiceCity.Contains(","))
                {
                    List<string> pincodes = new List<string>();
                    pincodes = item.ServiceCity.Split(",").ToList();
                    if (pincodes.Contains(userpincode))
                    {
                        ListProviders.Add(new User {Usid=item.Usid,Username=item.Username,Specialization=item.Specialization,Specification=item.Specification,Costperhour=item.Costperhour,Rating=item.Rating,Experience=item.Experience });
                    }
                }
            }
            return View(ListProviders);
        }
    }
}
