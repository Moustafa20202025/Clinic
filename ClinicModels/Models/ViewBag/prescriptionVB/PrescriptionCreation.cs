
namespace ClinicModels.Models.ViewBag.prescriptionVB
{
    public class PrescriptionCreation
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string DrugName { get; set; }
        public string DosageDescription { get; set; }
        public DateTime DateOfConsultation { get; set; }

    }
}
