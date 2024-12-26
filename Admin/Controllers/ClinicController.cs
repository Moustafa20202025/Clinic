using ClinicModels.Models;
using ClinicModels.Models.ViewBag.ClinicEmployeeVB;
using ClinicModels.Models.ViewBag.ClinicVB;
using ClinicModels.Models.ViewBag.SalaryEmpolyeeVB;
using ClinicModels.Models.ViewBag.ViewModel;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utitiy;
using X.PagedList.Extensions;




namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ClinicController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public ClinicController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index(string searchString, int? page)
        {
            var clinic = _unitOfWork.clinicRepository
                .GetAll(includeProperties: "doctor").Where(u => u.IsDeleted ==true);
            if (!string.IsNullOrEmpty(searchString))
            {
                clinic = clinic
                    .Where(d => d.ClinicName.Contains(searchString)
                    || d.doctor.FirstName.Contains(searchString)
                    || d.Address.Contains(searchString));
                   
                    
            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(clinic.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Create()
        {
            ViewBag.clinicList = _unitOfWork.doctorRepository.GetAll().Where(u => u.IsDeleted == true).Select(u => new SelectListItem

            {

                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            });
            var clinic = new ClinicCreation();

            return View(clinic);
        }
    
         [HttpPost]
         public IActionResult Create(ClinicCreation clinic)
         {
           
             if (ModelState.IsValid)
             {
                var clinicASEntity = new Clinic()
                {
                    DoctorId = clinic.DoctorId,
                    ClinicName = clinic.ClinicName,
                    Address = clinic.Address,
                    Date = clinic.Date,
                    OpeningTime = clinic.OpeningTime,
                    ClosingTime = clinic.ClosingTime,
                    IsOpen = clinic.IsOpen,
                    Notes = clinic.Notes,
                    WorkingDays = clinic.WorkingDays

                };
                _unitOfWork.clinicRepository.Add(clinicASEntity);
                 _unitOfWork.Save();
                 return RedirectToAction("Index");
             }
             return NotFound();
         }
        //_unitOfWork.clinicRepository.Update(clinicvm.clinic);
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Fetch the SalaryEmployee entity
            var clinic = _unitOfWork.clinicRepository.Get(x => x.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            // Create the SalaryEmployeeEdit ViewModel and populate its properties
            var clinicEdit = new ClinicEdit
            {
                Id = clinic.Id,
                DoctorId = clinic.DoctorId,
                ClinicName = clinic.ClinicName,
                Address =clinic.Address,
                Date = clinic.Date,
                OpeningTime = clinic.OpeningTime,
                ClosingTime = clinic.ClosingTime,
               IsOpen= clinic.IsOpen,
               WorkingDays= clinic.WorkingDays,
               Notes= clinic.Notes
            };

            // Populate the ViewBag with necessary data
            ViewBag.doctorList = _unitOfWork.doctorRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName} ",
                Value = u.Id.ToString()
            }).ToList();

            return View(clinicEdit);
        }


        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(ClinicEdit clinic)
        {

            if (ModelState.IsValid)
            {
                Clinic clinicAsEntity = new Clinic()
                {
                    Id = clinic.Id,
                    DoctorId = clinic.DoctorId,
                    ClinicName = clinic.ClinicName,
                    Address = clinic.Address,
                    Date = clinic.Date,
                    OpeningTime = clinic.OpeningTime,
                    ClosingTime = clinic.ClosingTime,
                    IsOpen = clinic.IsOpen,
                    WorkingDays = clinic.WorkingDays,
                    Notes = clinic.Notes

                };
               
                _unitOfWork.clinicRepository.Update(clinicAsEntity);
                _unitOfWork.Save();
                TempData["success"] = "Salary Employee Updated successfully";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }




        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Clinic clinic = _unitOfWork.clinicRepository.GetAll(includeProperties: "doctor").FirstOrDefault(u => u.Id == id);


            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Clinic? obj = _unitOfWork.clinicRepository.GetAll(includeProperties: "doctor").FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.IsDeleted = false;
            _unitOfWork.clinicRepository.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Clinic Deleted successfully";

            return RedirectToAction("Index");
        }

    }

} 

