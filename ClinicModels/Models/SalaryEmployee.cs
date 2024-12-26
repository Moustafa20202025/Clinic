using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicModels.Models
{
    public class SalaryEmployee
    {
        [Key] public int Id { get; set; }
        public int ClinicEmployeeId { get; set; }

        [Required] public decimal BaseSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal Bonuses { get; set; }
        public decimal NetSalary { get; set; }
        [Required][DataType(DataType.Date)] public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } 
        // e.g., Paid, Pending, etc.
        public string Remarks { get; set; }
        public bool IsDeleted { get; set; } = true;

        //Navigator Property
        [ForeignKey(nameof(ClinicEmployeeId))]
        public ClinicEmployee  clinicEmployee { get; set; }

        public void CalculateNetSalary() 
        {
            NetSalary = BaseSalary + Bonuses - Deductions;
        }
    }
}
