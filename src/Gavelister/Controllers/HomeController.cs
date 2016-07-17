using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gavelister.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Administration");
            } else return RedirectToAction("Index", "Gifts");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
