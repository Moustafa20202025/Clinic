using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModels.Models
{
    public class Patient
    {

        [Key] public int Id { get; set; }
        [Required][StringLength(100)] public string FirstName { get; set; }
        [Required][StringLength(100)] public string LastName { get; set; }
        [Required][DataType(DataType.Date)] public DateTime DateOfBirth { get; set; }
        [Required][Phone] public string Phone { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string BloodType { get; set; }
        [Required] public double Weight { get; set; }
        public string Analysis { get; set; }
        public string RequiredRays { get; set; }
        public string DiseaseStatus { get; set; }
        public string PatientComplaint { get; set; }
        public string PatientDiagnosis { get; set; }
        public bool IsDeleted { get; set; }= true;

        // Navigation properties
        public ICollection<Prescription> prescription { get; set; } = [];
        public ICollection<DoctorPatientSchedule> doctorPatientSchedule { get; set; } = [];
      


    }
}
