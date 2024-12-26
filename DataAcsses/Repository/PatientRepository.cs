using ClinicModels.Models;
using DataAcsses.Db;
using DataAcsses.Repository.IRepository;


namespace DataAcsses.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private AppDbContext _db;
        public PatientRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Patient obj)
        {
            _db.Patients.Update(obj);
        }
    }
}
