
namespace ClinicModels.Models.ViewBag.SalaryEmpolyeeVB
{
    public class EmployeeSalaryCreation
    {
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal Bonuses { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string Remarks { get; set; }


    }
}
