

namespace DataAcsses.Repository.IRepository
{
    public interface IUnitOfWork
    {
     IClinicRepository clinicRepository { get; }
     IClinicStoreRepository clinicStoreRepository { get; }
     IDoctorRepository doctorRepository { get; }
     IClinicEmployeeRepository clinicEmployeeRepository { get; }
     IPatientRepository patientRepository { get; }
     IPrescriptionRepositore prescriptionRepository { get; }
     ISalaryEmployeeRepository salaryEmployeeRepository { get; }

    void Save();
    }
}
