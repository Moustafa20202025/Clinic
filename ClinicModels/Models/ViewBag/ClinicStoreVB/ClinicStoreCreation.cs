

namespace ClinicModels.Models.ViewBag.ClinicStore
{
    public class ClinicStoreCreation
    {
        public int ClinicId { get; set; }
        public string InstrumentName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
