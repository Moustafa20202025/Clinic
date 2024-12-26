using ClinicModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcsses.Repository.IRepository
{
    public interface IPatientRepository: IRepository<Patient>
    {
       
            void Update(Patient obj);
            void Save();
        
    }
}
