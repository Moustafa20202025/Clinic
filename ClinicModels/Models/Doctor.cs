using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace ClinicModels.Models
{
    public class Doctor
    {
        [Key] public int Id { get; set; }
        [Required][StringLength(100)] public string FirstName { get; set; }
        [Required][StringLength(100)] public string LastName { get; set; }
        [Required][StringLength(200)] public string Specialty { get; set; }
        [Required][Phone] public string Phone { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Gender { get; set; }
        public string Qualifications { get; set; }
        public string Experience { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = true;
        // Navigation properties
        [JsonIgnore]
        public ICollection<Clinic> clinic { get; set; } = [];



        public ICollection<DoctorPatientSchedule> doctorPatientSchedule { get; set; } = [];

        public ICollection<Prescription> prescription { get; set; } = [];
      
        
        
      
    }
}
