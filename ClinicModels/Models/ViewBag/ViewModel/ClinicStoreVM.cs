using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicModels.Models
{
    public class ClinicStoreVM
    {
        public ClinicStore clinicstore { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> clinicstoreList { get; set; }
    }
}

