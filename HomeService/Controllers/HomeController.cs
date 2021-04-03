using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {
            string token = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44336/");
                var postData = httpclient.PostAsJsonAsync<Login>("api/Authentication/AuthenicateUser", user);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    token = await res.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    if (token != null)
                    {
                        return RedirectToAction("CheckRole", "Home",new { username=user.Username, password =user.Password});
                    }

                }
            }
            return View();

        }
        public async Task<IActionResult> CheckRole(string username,string password)
        {
            ///api/Users/{uname},{pass}
            Role role = new Role();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44322/");
                using (var response = await httpClient.GetAsync("/api/Users/"+username+","+password))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                     role = JsonConvert.DeserializeObject<Role>(apiResponse);
                }
                if (role.role==null)
                {
                    
                }
                if(role.role.ToLower()=="admin")
                {
                    return RedirectToAction("Index", "Admin",new { role.role });
                }
            }
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult SentMail()
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("wirenethomeservices@gmail.com");
            msg.To.Add("purushothaman.ramasamy@kanini.com");
            msg.Body = "Testing";
            msg.IsBodyHtml = true;
            msg.Subject = "Welcome to HomeServices";
            SmtpClient smt = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl=true,
                UseDefaultCredentials   =false,
                DeliveryMethod=SmtpDeliveryMethod.Network,
                Credentials=new NetworkCredential("wirenethomeservices@gmail.com", "Wirenet@123")
            };
            
            smt.Send(msg);
            return RedirectToAction("Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
