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
    
        public class ClinicStoreRepository : Repository<ClinicStore>, IClinicStoreRepository
        {
            private AppDbContext _db;
            public ClinicStoreRepository(AppDbContext db) : base(db)
            {
                _db = db;
            }

            public void Save()
            {
                _db.SaveChanges();
            }
            public void Update(ClinicStore obj)
            {
                _db.ClinicStores.Update(obj);
            }
        }
}

