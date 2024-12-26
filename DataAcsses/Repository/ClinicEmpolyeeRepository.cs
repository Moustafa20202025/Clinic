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
    public class ClinicEmployeeRepository : Repository<ClinicEmployee>, IClinicEmployeeRepository
    {



        private AppDbContext _db;
        public ClinicEmployeeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(ClinicEmployee obj)
        {
            _db.ClinicEmployees.Update(obj);
        }

    }
}
