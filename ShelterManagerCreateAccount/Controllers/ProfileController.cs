using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShelterManagerCreateAccount.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult EditShelterProfile()
        {
            return View();
        }
    }
}

