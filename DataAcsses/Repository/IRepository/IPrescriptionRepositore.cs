using ClinicModels.Models;


namespace DataAcsses.Repository.IRepository
{
    public interface IPrescriptionRepositore : IRepository<Prescription>
    {

        void Update(Prescription obj);
        void Save();

    }
}
