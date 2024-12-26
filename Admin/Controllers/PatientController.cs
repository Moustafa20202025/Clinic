using ClinicModels.Models;
using ClinicModels.Models.ViewBag.ViewModel;
using DataAcsses.Db;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using X.PagedList.Extensions;
using static ClinicModels.Models.ViewBag.ViewModel.PatientVM;

namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index(string searchString, int? page)
        {
            var patient = _unitOfWork.patientRepository
                .GetAll().Where(u => u.IsDeleted == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                patient = patient
                    .Where(d => d.FirstName.Contains(searchString)
                    || d.LastName.Contains(searchString) ||
                    d.Phone.Contains(searchString) ||
                    d.Email.Contains(searchString));
            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(patient.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Upsert(int id)
        {
            PatientVM patientVM = new()
            {
                GenderList = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = g.ToString()
                }).ToList(),
                patient = new Patient()
            };

            if (id == null || id == 0)
            {
                //create
                return View(patientVM);
            }
            else
            {
                //update
                patientVM.patient = _unitOfWork.patientRepository.Get(u => u.Id == id);
                return View(patientVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (patient.Id == 0)
                {
                    _unitOfWork.patientRepository.Add(patient);

                }
                else
                {
                    _unitOfWork.patientRepository.Update(patient);

                }

            }

            _unitOfWork.Save();


            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var patient = _unitOfWork.patientRepository.Get(u => u.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _unitOfWork.patientRepository.Get(u => u.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.IsDeleted = false;
            _unitOfWork.patientRepository.Update(patient);
            _unitOfWork.Save();

            TempData["success"] = "Patient deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}



