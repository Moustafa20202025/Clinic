using ClinicModels.Models;
using DataAcsses.Db;
using DataAcsses.Repository.IRepository;


namespace DataAcsses.Repository
{
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        private AppDbContext _db;
        public ClinicRepository(AppDbContext db) :base(db) 
        {
                _db=db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Clinic obj)
        {
            _db.Clinics.Update(obj);
        }
    }
}
