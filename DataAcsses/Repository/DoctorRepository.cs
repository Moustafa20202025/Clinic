using ClinicModels.Models;
using DataAcsses.Db;
using DataAcsses.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcsses.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private AppDbContext _db;
        public DoctorRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Doctor obj)
        {
            _db.Doctors.Update(obj);
        }
    }
}
