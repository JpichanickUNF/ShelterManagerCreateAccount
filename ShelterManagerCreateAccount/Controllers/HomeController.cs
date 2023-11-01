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

       // [HttpGet]
        public IActionResult ClientManager()
        {

            List<Client> Clients = new List<Client>();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;

            ClientContext cc = new ClientContext(connStr);


            ShelterLocationContext slc = new ShelterLocationContext(connStr);

            var shelterLocations = from c in slc.ShelterLocations orderby c.Shelter_Location_Description select c;

           // SelectList sl = new SelectList(shelterLocations,"Shelter_Location_ID","Shelter_Location_Description");
            ViewBag.ShelterLocations = shelterLocations;


            var query = from c in cc.Clients orderby c.L_Name select c;
            //var query = from c in cc.Clients 
            //            join s in cc.ShelterLocations on c.Shelter_Location_ID = ShelterLocation.
            //            orderby c.L_Name select c;
            List<Client> myData = query.ToList();
            //foreach (Client client in myData)
            //{
            //    client.Shelter_Locations = shelterLocations.ToList();

            //}

            return View(myData);
        }

        [HttpPost]
        [Route("ClientManagerUpdate")]
        public IActionResult ClientManagerUpdate([FromForm] List<Client> clients)
        {


            return View();

        }
        [Route("ClientDetailManager/{client_ID}")]
        public IActionResult ClientDetailManager(int Client_ID)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;

            ClientContext cc = new ClientContext(connStr);


            ShelterLocationContext slc = new ShelterLocationContext(connStr);
            var shelterLocations = from c in slc.ShelterLocations orderby c.Shelter_Location_Description select c;

      
            ViewBag.ShelterLocations = shelterLocations;

            Client  theClient = cc.Clients.Find(Client_ID);
           

            return View(theClient);
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