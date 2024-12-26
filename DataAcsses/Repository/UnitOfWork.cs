using DataAcsses.Db;
using DataAcsses.Repository.IRepository;


namespace DataAcsses.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public IClinicRepository clinicRepository { get; private set; }
        public IClinicStoreRepository clinicStoreRepository { get; private set; }

        public IDoctorRepository doctorRepository {  get; private set; }
        public IClinicEmployeeRepository clinicEmployeeRepository {  get; private set; }
        public IPatientRepository patientRepository {  get; private set; }

        public IPrescriptionRepositore prescriptionRepository { get; private set; }

        public ISalaryEmployeeRepository salaryEmployeeRepository { get; private set; }

        public UnitOfWork(AppDbContext db)
        {
           _db = db;
           clinicRepository = new ClinicRepository(_db);
           clinicStoreRepository = new ClinicStoreRepository(_db);
           doctorRepository = new DoctorRepository(_db);
           clinicEmployeeRepository = new ClinicEmployeeRepository(_db);
           patientRepository = new PatientRepository(_db);
           prescriptionRepository = new PrescriptionRepositore(_db);
           salaryEmployeeRepository = new SalaryEmployeeRepository(_db);   
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
