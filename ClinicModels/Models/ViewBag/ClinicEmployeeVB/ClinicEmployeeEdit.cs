using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModels.Models.ViewBag.ClinicEmployeeVB
{
    public class ClinicEmployeeEdit
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Email { get; set; }

        public enum GenderEnum
        {
            Male,
            Female
        }

        public enum MaritalStatusEnum
        {
            Single,
            Married,
            Divorced,
            Widowed

        }
    }
}
