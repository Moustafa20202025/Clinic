using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClinicModels.Models
{
    public class ClinicEmployee
    {
        [Key] 
        public int Id { get; set; }
        
        public int ClinicId { get; set; }
         
        [Required][StringLength(100)] public string FirstName { get; set; }
        [Required][StringLength(100)] public string LastName { get; set; }
        [Required][Phone] public string Phone { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string MaritalStatus { get; set; }
     
        public decimal currentSalary { get; set; }
       
       
        [Required][EmailAddress] public string Email { get; set; }
        public bool IsDeleted { get; set; }=true;

        // Navigation properties
        public Clinic clinic { get; set; }
        public ICollection<SalaryEmployee> salaryEmployee { get; set; } = []; 




    }
}
