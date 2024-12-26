using ClinicModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcsses.Repository.IRepository
{
    public interface IClinicEmployeeRepository: IRepository<ClinicEmployee>
    {
        
        
            void Update(ClinicEmployee obj);
            void Save();
        

    }
}
