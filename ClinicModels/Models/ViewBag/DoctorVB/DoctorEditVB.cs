﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModels.Models.ViewBag.DoctorVB
{
    public class DoctorEditVB
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Qualifications { get; set; }
        public string Experience { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
        public enum GenderEnum
        {
            Male,
            Female
        }
       
    }
}
