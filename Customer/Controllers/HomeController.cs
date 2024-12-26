using ClinicModels.Models;
using ClinicProject.Models;
using ClinicProject.Resources;
using DataAcsses.Repository.IRepository;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Http;
namespace ClinicProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly languageService _languageService;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork , languageService languageService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _languageService = languageService;
        }


        public IActionResult ChangeLang(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),

                });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Index() 
        {
            IEnumerable<Doctor> DoctorsList = _unitOfWork.doctorRepository
               .GetAll(includeProperties: "clinic")
               .Where(x => x.IsDeleted == true).ToList();
            

            var Privacy = _languageService.GetLocalizedHTML("Privacy").Value;
            var Clinic = _languageService.GetLocalizedHTML("Clinic").Value;
            var Home = _languageService.GetLocalizedHTML("Home").Value;
            var ClinicProject = _languageService.GetLocalizedHTML("ClinicProject").Value;
            var Language = _languageService.GetLocalizedHTML("Language").Value;
            var Details = _languageService.GetLocalizedHTML("Details").Value;
            var Register = _languageService.GetLocalizedHTML("Register").Value;
           

           
            ViewData["Privacy"] = Privacy;
            ViewData["Clinic"] = Clinic;
            ViewData["Home"] = Home;
            ViewData["ClinicProject"] = ClinicProject;
            ViewData["Language"] = Language;
            ViewData["Details"] = Details;
            ViewData["Register"] = Register;

     
            return View(DoctorsList); 
        }
    
        
        public IActionResult Details(int doctorId)
        {


            Doctor product = _unitOfWork.doctorRepository.Get(u => u.Id == doctorId, includeProperties: "clinic");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
