using ClinicModels.Models;
using ClinicModels.Models.ViewBag.ViewModel;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;
using ClinicModels.Models.ViewBag.ClinicEmployeeVB;
using static ClinicModels.Models.ViewBag.ClinicEmployeeVB.ClinicEmpolyeeCreation;
using ClinicModels.Models.ViewBag.SalaryEmpolyeeVB;


namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClinicEmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClinicEmployeeController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(string searchString, int? page)
        {
            var clinicEmployee = _unitOfWork.clinicEmployeeRepository
                .GetAll(includeProperties: "salaryEmployee").Where(u => u.IsDeleted == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                clinicEmployee = clinicEmployee
                    .Where(d => d.FirstName.Contains(searchString)
                    || d.LastName.Contains(searchString) ||
                    d.Phone.Contains(searchString) ||
                    d.Email.Contains(searchString));
            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(clinicEmployee.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Create()
        {


                ViewBag.ClinicList = _unitOfWork.clinicRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ClinicName,
                    Value = u.Id.ToString()
                });

                ViewBag.GenderList = Enum.GetValues(typeof(GenderEnum)) .Cast<GenderEnum>().Select(g => new SelectListItem
                   
                { 
                    Text = g.ToString(),
                    Value = g.ToString() 
                }).ToList();
               
                ViewBag.MaritalStatusList = Enum.GetValues(typeof(MaritalStatusEnum)).Cast<MaritalStatusEnum>().Select(ms => new SelectListItem 
                    
                {
                   Text = ms.ToString(),
                   Value = ms.ToString() 
                }).ToList();
               
                var clinicEmployee = new ClinicEmpolyeeCreation();

            return View(clinicEmployee);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClinicEmpolyeeCreation clinicEmployee)
        {
    
            if (ModelState.IsValid)
            {
                var employee = new ClinicEmployee()
                {
                    ClinicId = clinicEmployee.ClinicId,
                    FirstName = clinicEmployee.FirstName,
                    LastName = clinicEmployee.LastName,
                    Phone = clinicEmployee.Phone,
                    Email = clinicEmployee.Email,
                    Gender = clinicEmployee.Gender,
                    MaritalStatus = clinicEmployee.MaritalStatus
                };
                _unitOfWork.clinicEmployeeRepository.Add(employee);
                _unitOfWork.Save();
                TempData["success"] = "Clinic Employee created successfully";
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public IActionResult Edit (int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ClinicEmployee = _unitOfWork.clinicEmployeeRepository.Get(x => x.Id == id);
            if (ClinicEmployee == null)
            {
                return NotFound();
            }
            var salaryEmployeeEdit = new ClinicEmployeeEdit
            {
                Id = ClinicEmployee.Id,
                ClinicId = ClinicEmployee.ClinicId,
                FirstName = ClinicEmployee.FirstName,
                LastName = ClinicEmployee.LastName,
                Phone = ClinicEmployee.Phone,
                Email = ClinicEmployee.Email,
                Gender = ClinicEmployee.Gender,
                MaritalStatus = ClinicEmployee.MaritalStatus
            };

            ViewBag.ClinicList = _unitOfWork.clinicRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ClinicName,
                Value = u.Id.ToString()
            });

            ViewBag.GenderList = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(g => new SelectListItem

            {
                Text = g.ToString(),
                Value = g.ToString()
            }).ToList();

            ViewBag.MaritalStatusList = Enum.GetValues(typeof(MaritalStatusEnum)).Cast<MaritalStatusEnum>().Select(ms => new SelectListItem

            {
                Text = ms.ToString(),
                Value = ms.ToString()
            }).ToList();
            //_unitOfWork.clinicEmployeeRepository.Update(clinicEmployee);
            //_unitOfWork.Save();
            return View(salaryEmployeeEdit);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(ClinicEmployeeEdit clinicEmployee)
        {

            if (ModelState.IsValid)
            {
                ClinicEmployee s = new ClinicEmployee()
                {
                    Id=clinicEmployee.Id,
                    ClinicId = clinicEmployee.ClinicId,
                    FirstName = clinicEmployee.FirstName,
                    LastName = clinicEmployee.LastName,
                    Phone = clinicEmployee.Phone,
                    Email = clinicEmployee.Email,
                    Gender = clinicEmployee.Gender,
                    MaritalStatus = clinicEmployee.MaritalStatus

                };
                
               _unitOfWork.clinicEmployeeRepository.Update(s);
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
            ClinicEmployee employeeFormDb = _unitOfWork.clinicEmployeeRepository.GetAll().FirstOrDefault(u => u.Id == id);


            if (employeeFormDb == null)
            {
                return NotFound();
            }
            return View(employeeFormDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            ClinicEmployee? obj = _unitOfWork.clinicEmployeeRepository.GetAll().FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.IsDeleted = false;
            _unitOfWork.clinicEmployeeRepository.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "ClinicEmployee deleted successfully";

            return RedirectToAction(nameof(Index));


        }
    }
}
