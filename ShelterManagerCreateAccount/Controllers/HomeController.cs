﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Versioning;
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
        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult FAQ()
        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;

            FAQContext ff = new FAQContext(connStr);


            var query = from f in ff.FrequentlyAskedQuestions
                        orderby f.QuestionID
                        select f;

            List<FrequentlyAskedQuestions> myData = query.ToList();
            return View(myData);

        }

        public IActionResult ContactPage()
        {
            return View();
        }
        public IActionResult EditShelterProfile()
        {
            return View();
        }

        public IActionResult ClientManager()
        {

            List<Client> Clients = new List<Client>();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;

            ClientContext cc = new ClientContext(connStr);


            ShelterLocationContext slc = new ShelterLocationContext(connStr);
            var shelterLocations = from c in slc.ShelterLocations orderby c.Shelter_Location_Description select c;
             SelectList sl = new SelectList(shelterLocations,"Shelter_Location_ID","Shelter_Location_Description");
            ViewBag.ShelterLocations = shelterLocations;

            var query = from c in cc.Clients
                        join s in cc.ShelterLocations on c.Shelter_Location_ID equals s.Shelter_Location_ID
                        orderby c.L_Name
                        select c;

            List<Client> myData = query.ToList();

            return View(myData);
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
            Client theClient = cc.Clients.Find(Client_ID);

            if(theClient == null)
            {
                theClient = new Client();
            }

            return View(theClient);
        }

        [HttpPost]
        [Route("ClientDetailManager/{client_ID}")]
        public IActionResult ClientDetailManager([FromForm] Client c)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;
            ViewBag.ErrorMessage = "";

            //if (ModelState.IsValid)
            //{


                if (c.ClientID == 0)
                {
                    //no client id, therefore insert
                    using (ClientContext cc = new ClientContext(connStr))
                    {
                        cc.Clients.Add(c);
                        cc.SaveChanges();
                    }
                }
                else
                {
                    //have client id, so update
                    using (ClientContext db = new ClientContext(connStr))
                    {
                        db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return View(null);
            //}
            //else
            //{
            //    ViewBag.ErrorMessage = "First Name and Last Name are required!";
            //    return View(c);
            //}
            

        }

        [HttpPost]
        [Route("DeleteClient")]
        public JsonResult DeleteClient(int clientIDVal)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string connStr = config.GetSection("Connnectionstrings:MyConnection").Value;
            Client c = null;

            using (ClientContext cc = new ClientContext(connStr))
            {
                c = cc.Clients.Find(clientIDVal);
            }

            using (ClientContext cc = new ClientContext(connStr))
            {
                cc.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                cc.SaveChanges();
            }

            return Json(new { Success = false, Message = "Delete failed." });

        }

        public IActionResult Create()
        {
            return View();
        }

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