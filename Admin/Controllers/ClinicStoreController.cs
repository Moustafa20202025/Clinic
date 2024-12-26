using ClinicModels.Models;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;


namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClinicStoreController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ClinicStoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        public IActionResult Index(string searchString, int? page)
        {
            TempData["success"] = "Operation completed successfully";
            var clinicStore = _unitOfWork.clinicStoreRepository
                .GetAll(includeProperties: "clinic").Where(u => u.IsDeleted == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                clinicStore = clinicStore
                       .Where(d => d.clinic.ClinicName.Contains(searchString)
                    || d.InstrumentName.Contains(searchString));



            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(clinicStore.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Upsert(int id)
        {

            ClinicStoreVM clinicstoreVM = new()
            {
                clinicstoreList = _unitOfWork.clinicRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ClinicName,
                    Value = u.Id.ToString()
                }),
                clinicstore = new ClinicStore()
            };


            if (id == null || id == 0)
            {
                //create
                return View(clinicstoreVM);
            }
            else
            {
                //update
                clinicstoreVM.clinicstore = _unitOfWork.clinicStoreRepository.Get(u => u.Id == id);
                return View(clinicstoreVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ClinicStoreVM clinicStorevm)
        {
            if (clinicStorevm.clinicstore.Id == 0)
            {
                _unitOfWork.clinicStoreRepository.Add(clinicStorevm.clinicstore);

            }
            else
            {
                _unitOfWork.clinicStoreRepository.Update(clinicStorevm.clinicstore);

            }

            _unitOfWork.Save();
            TempData["success"] = "Clinic Store created/updated successfully";
            return RedirectToAction("Index");




        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    ClinicStore ClinicStoreDb = _unitOfWork.clinicStoreRepository.Get(u => u.Id == id);


        //    if (ClinicStoreDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ClinicStoreDb);

        //}
        //[HttpPost]
        //public IActionResult Edit(ClinicStore obj)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        _unitOfWork.clinicStoreRepository.Update(obj);
        //        _unitOfWork.clinicStoreRepository.Save();


        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ClinicStore ClinicStoreFormDb = _unitOfWork.clinicStoreRepository.Get(u => u.Id == id, includeProperties: "clinic");


            if (ClinicStoreFormDb == null)
            {
                return NotFound();
            }
            return View(ClinicStoreFormDb);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {

            ClinicStore obj = _unitOfWork.clinicStoreRepository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.IsDeleted = false;
            _unitOfWork.clinicStoreRepository.Update(obj);
            _unitOfWork.Save();

            TempData["success"] = "Clinic Store deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
