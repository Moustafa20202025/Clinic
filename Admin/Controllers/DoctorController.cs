
using Microsoft.AspNetCore.Mvc;
using ClinicModels.Models;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;
using Microsoft.AspNetCore.Authorization;
using Utitiy;
using static ClinicModels.Models.ViewBag.DoctorVB.DoctorCreationVB;
using ClinicModels.Models.ViewBag.DoctorVB;
using System.Numerics;
using DataAcsses.Repository;



namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;



        public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string searchString, int? page)
        {
            var doctors = _unitOfWork.doctorRepository
                .GetAll().Where(u => u.IsDeleted == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                doctors = doctors
                    .Where(d => d.FirstName.Contains(searchString)
                    || d.LastName.Contains(searchString) ||
                    d.Specialty.Contains(searchString) ||
                    d.Email.Contains(searchString));
            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(doctors.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Create()
        {
    
                ViewBag.GenderList = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = g.ToString()
                }).ToList();

            var Doctorf = new DoctorCreationVB();
            
            return View(Doctorf);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorCreationVB doctor, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                
                
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string finalPath = Path.Combine(wwwRootPath, @"images\doctor");
                
                    if (!string.IsNullOrEmpty(doctor.ImageUrl))
                    {
                        var oldImagepath = Path.Combine(wwwRootPath, doctor.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagepath))
                        {
                            System.IO.File.Delete(oldImagepath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                
                    doctor.ImageUrl = @"\images\doctor\" + fileName;
                   
                }
           
                var doctorEntity = new Doctor()
                {
                    FirstName= doctor.FirstName,
                    LastName= doctor.LastName,
                    Specialty= doctor.Specialty,
                    Phone= doctor.Phone,
                    Email= doctor.Email,
                    Gender= doctor.Gender,
                    Qualifications= doctor.Qualifications,
                    Experience= doctor.Experience,
                    ImageUrl= doctor.ImageUrl
                };
           
                _unitOfWork.doctorRepository.Add(doctorEntity);
                _unitOfWork.Save();
                TempData["success"] = "Doctor created successfully";
                return RedirectToAction("Index");
            }

            return NotFound();

        }

        public IActionResult Edit(int id)
        {
            if(id== 0 && id==null)
            {
                NotFound();
            }
            // Fetch the Doctors entity

            var doctor = _unitOfWork.doctorRepository.Get(x => x.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            var doctorEntity = new Doctor()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialty = doctor.Specialty,
                Phone = doctor.Phone,
                Email = doctor.Email,
                Gender = doctor.Gender,
                Qualifications = doctor.Qualifications,
                Experience = doctor.Experience,
                ImageUrl = doctor.ImageUrl
            };

            ViewBag.GenderList = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(g => new SelectListItem
            {
                Text = g.ToString(),
                Value = g.ToString()
            }).ToList();

            return View();  
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(DoctorEditVB doctor ,IFormFile? file )
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string finalPath = Path.Combine(wwwRootPath, @"images\doctor"); if (!string.IsNullOrEmpty(doctor.ImageUrl)) { var oldImagepath = Path.Combine(wwwRootPath, doctor.ImageUrl.TrimStart('\\')); if (System.IO.File.Exists(oldImagepath)) { System.IO.File.Delete(oldImagepath); } }
                    using (var fileStream = new FileStream
                        (Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    doctor.ImageUrl = @"\images\doctor\" + fileName;
                }
                var doctorEntity = new Doctor()
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Specialty = doctor.Specialty,
                    Phone = doctor.Phone,
                    Email = doctor.Email,
                    Gender = doctor.Gender,
                    Qualifications = doctor.Qualifications,
                    Experience = doctor.Experience,
                    ImageUrl = doctor.ImageUrl
                };
                _unitOfWork.doctorRepository.Update(doctorEntity);
                _unitOfWork.Save();
                TempData["success"] = "Doctor Updated successfully";
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var doctor = _unitOfWork.doctorRepository.Get(u => u.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _unitOfWork.doctorRepository.Get(u => u.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.IsDeleted = false;
            _unitOfWork.doctorRepository.Update(doctor);
            _unitOfWork.Save();

            TempData["success"] = "Doctor deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
