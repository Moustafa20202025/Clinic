using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModels.Models.ViewBag.ViewModel
{
    public class PatientVM
    {
        public Patient patient { get; set; }
        public enum Gender
        {
            Male,
            Female
        }

        public IEnumerable<SelectListItem> GenderList { get; set; }
    }
}
