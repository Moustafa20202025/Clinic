
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicModels.Models
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
       
       
        [Required]
        [StringLength(100)]
        public string ClinicName { get; set; }
        [Required]
        [StringLength(200)]
        public string Address{ get; set; }//
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }//
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan OpeningTime { get; set; }//
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan ClosingTime { get; set; }//
        [Required]
        public bool IsOpen { get; set; }= true;//
        [Required]
        [StringLength(100)]
        public string Notes { get; set; }//
        [Required]
        [StringLength(100)]
        public string WorkingDays { get; set; }//p
        public bool IsDeleted { get; set; } = true;

        // Navigation properties
        [ForeignKey(nameof(DoctorId))]
        public Doctor doctor { get; set; }

     
       
        public ICollection<ClinicEmployee> clinicEmployee { get; set; } = [];
        


 
    }
}
