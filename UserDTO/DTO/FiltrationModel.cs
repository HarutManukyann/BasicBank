using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class FiltrationModel
    {
        public string? Name { get; set; }=null;
        public string? Surname { get; set; } = null;
        public string? Passport { get; set; } = null;
        public DateTime StartDate { get; set; } = new DateTime(1, 1, 1);
        public DateTime EndDate { get; set; }=DateTime.Now;
        public string? Address { get; set; } = null;

        public string Passport { get; set; }
    }
}
