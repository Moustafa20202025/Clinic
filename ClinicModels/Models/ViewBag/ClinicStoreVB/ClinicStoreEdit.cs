using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModels.Models.ViewBag.ClinicStore
{
    public class ClinicStoreEdit
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string InstrumentName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
