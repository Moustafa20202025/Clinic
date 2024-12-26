using ClinicModels.Models;


namespace DataAcsses.Repository.IRepository
{
    public interface IClinicRepository:IRepository<Clinic>
    {
        void Update(Clinic obj);
        void Save();
    }
}
