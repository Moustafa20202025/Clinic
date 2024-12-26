
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicModels.Models
{
    public class DoctorPatientSchedule
    {

        [Key] public int Id { get; set; }
        [Required] public int DoctorId { get; set; }
        [Required] public int PatientId { get; set; }

        public bool IsDeleted { get; set; }
        // Navigation properties
        [ForeignKey(nameof(DoctorId))]
        public Doctor doctor { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient patient { get; set; } 
   
    }
}
