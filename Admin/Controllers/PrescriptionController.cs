using ClinicModels.Models;
using ClinicModels.Models.ViewBag.prescriptionVB;
using DataAcsses.Db;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;

namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PrescriptionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionController(IUnitOfWork unitOfWork, AppDbContext db)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index(string searchString, int? page)
        {
            var prescription = _unitOfWork.prescriptionRepository.GetAll(includeProperties: "doctor,patient").Where(u => u.IsDeleted == true).ToList();




            if (!string.IsNullOrEmpty(searchString))
            {
                prescription = prescription

                        .Where(d => d.doctor.FirstName.Contains(searchString)
                        || d.doctor.LastName.Contains(searchString) ||
                        d.patient.FirstName.Contains(searchString) ||
                        d.patient.LastName.Contains(searchString) ||
                        d.patient.Phone.Contains(searchString)).ToList();

            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(prescription.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.doctorList = _unitOfWork.doctorRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToList();

            ViewBag.patientList = _unitOfWork.patientRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToList();

            return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrescriptionCreation prescription)
        {
            Prescription currntNewPrescription = new Prescription()
            {
                PatientId = prescription.PatientId,
                DoctorId = prescription.DoctorId,
                DrugName = prescription.DrugName,
                DosageDescription = prescription.DosageDescription,
                DateOfConsultation = prescription.DateOfConsultation
            };

            if (ModelState.IsValid)
            {
                _unitOfWork.prescriptionRepository.Add(currntNewPrescription);
                _unitOfWork.Save();
                TempData["success"] = "Prescription created successfully";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var prescription = _unitOfWork.prescriptionRepository.Get(x => x.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }
            // Create the SalaryEmployeeEdit ViewModel and populate its properties
            var prescriptionEdit = new PrescriptionEdit
            {
                Id = prescription.Id,
                PatientId = prescription.PatientId,
                DoctorId = prescription.DoctorId,
                DrugName = prescription.DrugName,
                DosageDescription = prescription.DosageDescription,
                DateOfConsultation = prescription.DateOfConsultation
               
            };

            ViewBag.doctorList = _unitOfWork.doctorRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToList();

            ViewBag.patientList = _unitOfWork.patientRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToList();

        
            return View(prescriptionEdit);



        }
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(PrescriptionEdit prescription)
        {
            if (ModelState.IsValid)
            {
                var prescriptionEdit = new Prescription()
                {
                    Id = prescription.Id,
                    PatientId = prescription.PatientId,
                    DoctorId = prescription.DoctorId,
                    DrugName = prescription.DrugName,
                    DosageDescription = prescription.DosageDescription,
                    DateOfConsultation = prescription.DateOfConsultation
                };
                
                    _unitOfWork.prescriptionRepository.Update(prescriptionEdit);
                    _unitOfWork.Save();
                    TempData["success"] = "Prescription Updated successfully";
                
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Prescription prescriptionFormDb = _unitOfWork.prescriptionRepository.GetAll(includeProperties: "doctor,patient").FirstOrDefault(u => u.Id == id);


            if (prescriptionFormDb == null)
            {
                return NotFound();
            }
            return View(prescriptionFormDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Prescription? obj = _unitOfWork.prescriptionRepository.GetAll(includeProperties: "doctor,patient").FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.IsDeleted = false;
            _unitOfWork.prescriptionRepository.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Prescription Deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
