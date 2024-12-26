using ClinicModels.Models;
using ClinicModels.Models.ViewBag.SalaryEmpolyeeVB;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;

namespace ClinicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalaryEmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalaryEmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(string searchString, int? page)
        {
            var salaryEmployee = _unitOfWork.salaryEmployeeRepository
                .GetAll(includeProperties: "clinicEmployee").Where(u => u.IsDeleted == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                salaryEmployee = salaryEmployee
                    .Where(d => d.clinicEmployee.FirstName.Contains(searchString)
                    || d.clinicEmployee.LastName.Contains(searchString));


            }
            int pageSize = 10; // عدد العناصر في كل صفحة
            int pageNumber = page ?? 1;  //الصفحة الحالية 
            return View(salaryEmployee.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Create()
        {


            ViewBag.EmployeeList = _unitOfWork.clinicEmployeeRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToList();

            

            

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeSalaryCreation salaryEmployee)
        {


            var Employee = _unitOfWork.clinicEmployeeRepository.Get(u => u.Id == salaryEmployee.EmployeeId);
                
            if (ModelState.IsValid)
            {
                SalaryEmployee s = new SalaryEmployee()
                {
                    ClinicEmployeeId = salaryEmployee.EmployeeId,
                    BaseSalary = salaryEmployee.BaseSalary,
                    Deductions = salaryEmployee.Deductions,
                    PaymentDate = salaryEmployee.PaymentDate,
                    Remarks = salaryEmployee.Remarks,
                    PaymentStatus = salaryEmployee.PaymentStatus,
                    Bonuses = salaryEmployee.Bonuses

                };  
                s.CalculateNetSalary();
                _unitOfWork.salaryEmployeeRepository.Add(s);
                _unitOfWork.Save();
                TempData["success"] = "Salary Employee created successfully";
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

            // Fetch the SalaryEmployee entity
            var salaryEmployee = _unitOfWork.salaryEmployeeRepository.Get(x => x.Id == id);
            if (salaryEmployee == null)
            {
                return NotFound();
            }

            // Create the SalaryEmployeeEdit ViewModel and populate its properties
            var salaryEmployeeEdit = new SalaryEmployeeEdit
            {
                Id = salaryEmployee.Id,
                EmployeeId = salaryEmployee.ClinicEmployeeId,
                BaseSalary = salaryEmployee.BaseSalary,
                Deductions = salaryEmployee.Deductions,
                PaymentDate = salaryEmployee.PaymentDate,
                Remarks = salaryEmployee.Remarks,
                PaymentStatus = salaryEmployee.PaymentStatus,
                Bonuses = salaryEmployee.Bonuses
            };

            // Populate the ViewBag with necessary data
            ViewBag.EmployeeList = _unitOfWork.clinicEmployeeRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.FirstName} {u.LastName}",
                Value = u.Id.ToString()
            }).ToList();

            return View(salaryEmployeeEdit);
        }

  
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(SalaryEmployeeEdit salaryEmployee)
        {

            if (ModelState.IsValid)
            {
                SalaryEmployee s = new SalaryEmployee()
                {
                    Id = salaryEmployee.Id,
                    ClinicEmployeeId = salaryEmployee.EmployeeId,
                    BaseSalary = salaryEmployee.BaseSalary,
                    Deductions = salaryEmployee.Deductions,
                    PaymentDate = salaryEmployee.PaymentDate,
                    Remarks = salaryEmployee.Remarks,
                    PaymentStatus = salaryEmployee.PaymentStatus,
                    Bonuses = salaryEmployee.Bonuses

                };
                s.CalculateNetSalary();
                _unitOfWork.salaryEmployeeRepository.Update(s);
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
            SalaryEmployee salaryEmplyoeeFormDb = _unitOfWork.salaryEmployeeRepository.GetAll(includeProperties: "clinicEmployee").FirstOrDefault(u => u.Id == id);


            if (salaryEmplyoeeFormDb == null)
            {
                return NotFound();
            }
            return View(salaryEmplyoeeFormDb);



        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            SalaryEmployee? obj = _unitOfWork.salaryEmployeeRepository.GetAll(includeProperties: "clinicEmployee").FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.IsDeleted = false;
            _unitOfWork.salaryEmployeeRepository.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Salary Employee Deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
