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
    public class PrescriptionRepositore : Repository<Prescription>, IPrescriptionRepositore
    {
        private AppDbContext _db;
        public PrescriptionRepositore(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Prescription obj)
        {
            _db.Prescriptions.Update(obj);
        }
    }
}
