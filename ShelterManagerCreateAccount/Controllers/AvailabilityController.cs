using Microsoft.AspNetCore.Mvc;
using ShelterManagerCreateAccount.Models;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace ShelterManagerCreateAccount.Controllers
{
    public class AvailabilityController : Controller
    {
        
        private List<Availability> shltr;
        private int cotAmmount = 30;
        public AvailabilityController()
        {
            shltr = new List<Availability>()
            {
                new Availability(){Shelter = "Shelter1", Cots = cotAmmount, Info = "href='www.youtube.com'",},
                new Availability(){Shelter = "Shelter2", Cots = cotAmmount, Info = "href='www.youtube.com'"},
                new Availability(){Shelter = "Shelter3", Cots = cotAmmount, Info = "href='www.youtube.com'",},
            };
        }

        public IActionResult ClientView()
        {
            return View(shltr);
        }
        [HttpPost]
    }
}
