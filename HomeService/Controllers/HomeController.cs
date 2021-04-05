using HomeService.Models;
using HomeService.Models.ViewModel;
using Microsoft.AspNetCore.Http;
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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HomeService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetMobile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetMobile(VerifyMobile mobile)
        {
            User usr = new User();
            if (ModelState.IsValid)
            {
                string token = "";
                using (var httpclient = new HttpClient())
                {
                    /*StringContent content = new StringContent(JsonConvert.SerializeObject(mobile), Encoding.UTF8, "application/json")*/;

                        
                    using (var postData = httpclient.PostAsJsonAsync<VerifyMobile>("https://localhost:44336/api/Authentication/IsuserExists", mobile))
                    {
                        var res = postData.Result;
                        if (res.IsSuccessStatusCode)
                        {
                            token = await res.Content.ReadAsStringAsync();
                            HttpContext.Session.SetString("mobile", token);
                            if (token != null)
                            {
                                return RedirectToAction("Login");
                            }

                        }
                    }
                }
                TempData["registermobile"] = mobile.Phoneno;
                return RedirectToAction("Registration", "Registration");
            }
            return View();
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("mobile") == null)
            {
                return RedirectToAction("GetMobile");
            }
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
                    HttpContext.Session.SetString("IsvalidUser", token);

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
                if (HttpContext.Session.GetString("IsvalidUser") == null)
                {
                return RedirectToAction("Login");
                }   

                User user = new User();
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:44322/");
                    using (var response = await httpClient.GetAsync("/api/Users/Username/" + username))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                if (user.Role.ToLower() == "user")
                {
                    TempData["UserDetails"] = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("IsUserLoggedin", "user");

                    return RedirectToAction("Index", "Users");
                }
                if (user.Role.ToLower() == "provider")
                {
                    TempData["ProviderDetails"] = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("IsProviderLoggedin", "Provider");
                    return RedirectToAction("Index", "ServiceProvider");
                }
                if (user.Role.ToLower() == "admin")
                    {
                    HttpContext.Session.SetString("IsAdminLoggedin", "admin");
                    return RedirectToAction("Index", "Admin");
                    }
                }
                return RedirectToAction("Login", "Home");
            
           
        }
        public async Task<bool> IsUserNameExist(string Username, int? id)
        {
            //var validateName = db.ServiceProviders.FirstOrDefault(x => x.ElectricianID == ElectricianID && x.Sid != id);
            User validateName = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users/Username/" + Username))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    validateName = JsonConvert.DeserializeObject<User>(apiResponse);
                }

            }
            if (validateName.Usid != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
       /* public ActionResult SendSMS()
        {
            const string accountSid = "AC13049f464eed16fc464d8efa546e3845";
            const string authToken = "168f907ae0df650eedec38b4ceb70d19";

            TwilioClient.Init(accountSid, authToken);
            MessageResource.Create(
                to: new PhoneNumber("+919344418426"),
                from: new PhoneNumber("+1 201 335 0118"),
                body: "Ahoy from Twilio!");
            return View();
        }*/
        public async Task<bool> IsUserAadhaarExist(string Aadhaarno, int? id)
        {
            //var validateName = db.ServiceProviders.FirstOrDefault(x => x.ElectricianID == ElectricianID && x.Sid != id);
            User validateName = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44322/api/Users/Aadhaar/" + Aadhaarno))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    validateName = JsonConvert.DeserializeObject<User>(apiResponse);
                }

            }
            if (validateName!= null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public IActionResult Index()
        {
            
            return View();
        }
        /*public IActionResult SentMail()
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
        }*/
        public ActionResult DisplayMessage(string msg, string act, string ctrl, bool isinput, string id = "")
        {
            Message message = new Message();
            message.DispalyMessage = msg;
            message.ToAction = act;
            message.ToControl = ctrl;
            message.IsInput = isinput;
            message.Id = id;
            message.Inputdata = "Invalid Credentials";
            return View(message);
        }
    }
}
