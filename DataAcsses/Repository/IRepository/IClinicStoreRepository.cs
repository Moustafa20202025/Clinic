

using ClinicModels.Models;

namespace DataAcsses.Repository.IRepository
{
    public interface IClinicStoreRepository : IRepository<ClinicStore>
    {
        void Update(ClinicStore obj);
        void Save();
    }
}