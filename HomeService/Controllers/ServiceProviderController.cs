using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Controllers
{
    public class ServiceProviderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
