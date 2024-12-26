
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ClinicModels.Models
{
    public class ClinicStore
    {
        [Key] public int Id { get; set; }
        public int ClinicId { get; set; }
        [Required][StringLength(100)] public string InstrumentName { get; set; }
        [Required] public int Quantity { get; set; }
        [Required][DataType(DataType.Date)] public DateTime DateAdded { get; set; }
        [Required][DataType(DataType.Date)] public DateTime ExpiryDate { get; set; }
        public bool IsDeleted { get; set; } = true;
        // Navigation properties
        [ForeignKey(nameof(ClinicId))]
        public Clinic clinic { get; set; } 
      
    
    }
}
