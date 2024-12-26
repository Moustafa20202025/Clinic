using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModels.Models.ViewBag.ClinicVB
{
   public class ClinicEdit
    {

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string ClinicName { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public bool IsOpen { get; set; } = true;
        public string Notes { get; set; }
        public string WorkingDays { get; set; }
    }
}
