using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShelterManagerCreateAccount.Models
{
    public class Availability
    {
        public string Shelter {  get; set; }

        public int Cots { get; set; }

        public string Info { get; set; }

    }
}
