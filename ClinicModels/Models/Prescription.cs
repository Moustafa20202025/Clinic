
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicModels.Models
{
    public class Prescription
    {
        [Key] 
        public int Id { get; set; }
        public int  PatientId { get; set; }
        public int  DoctorId { get; set; }
        [Required][StringLength(100)] public string DrugName { get; set; }
        [Required][StringLength(200)] public string DosageDescription { get; set; }
        [Required][DataType(DataType.Date)] public DateTime DateOfConsultation { get; set; }
        public bool IsDeleted { get; set; } = true;
        [Required]
        [StringLength(100)]
        //Navigator Propertys
        [ForeignKey(nameof(DoctorId))]
        public Doctor doctor { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient patient { get; set; } 
      




    }
}
