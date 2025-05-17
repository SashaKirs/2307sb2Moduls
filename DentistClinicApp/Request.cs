using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentistClinicApp
{
    public class Request
    {
        public int Id { get; set; }
        public string Articul { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string FullDescription { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}

