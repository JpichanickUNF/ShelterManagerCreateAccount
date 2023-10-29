using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShelterManagerCreateAccount.DataAccess;
using ShelterManagerCreateAccount.Models;
using System.Diagnostics;

namespace ShelterManagerCreateAccount.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult ContactPage()
        {
            return View();
        }

        public IActionResult ClientManager()
        {

            List<Client> Clients = new List<Client>();

            //Client c1 = new Client();
            //c1.F_Name = "FTest";
            //c1.ClientID = 7;
            //c1.L_Name = "LTest";
            //Clients.Add(c1);

            //Client c2 = new Client();
            //c2.F_Name = "XTest";
            //c2.ClientID = 8;
            //c2.L_Name = "XLTest";
            //Clients.Add(c2);

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;

            ClientContext cc = new ClientContext(connStr);
            //Client c2 = new Client();
            //c2.F_Name = "XTest";
            //c2.L_Name = "XLTest";
            //c2.Shelter_Location_ID = 2;
            //cc.Clients.Add(c2);
            //cc.SaveChanges();

            ShelterLocationContext slc = new ShelterLocationContext(connStr);

            var shelterLocations = from c in slc.ShelterLocations orderby c.Shelter_Location_Description select c;

            SelectList sl = new SelectList(shelterLocations,"Shelter_Location_ID","Shelter_Location_Description");



            ViewBag.ShelterLocations = sl;


            var query = from c in cc.Clients orderby c.L_Name select c;
            //var query = from c in cc.Clients 
            //            join s in cc.ShelterLocations on c.Shelter_Location_ID = ShelterLocation.
            //            orderby c.L_Name select c;
            var myData = query.ToList();
            

            return View(myData);
        }

        public IActionResult Create()
        {
            return View();
        }

        //This should fix the error.
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult LoginView()
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