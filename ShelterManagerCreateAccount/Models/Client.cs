using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShelterManagerCreateAccount.Models

{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public string F_Name { get; set; }
        public string M_Name { get; set; }
        public string L_Name { get; set; }

        public int Shelter_Location_ID { get; set; }

        public List<ShelterLocation> Shelter_Locations { get; set; }
    }
}
