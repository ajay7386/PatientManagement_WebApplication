using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManagement.Common;
using PatientManagement.ExternalService.Wrapper;
using PatientManagement.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PatientManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWrapper _wrapper;

        public HomeController(ILogger<HomeController> logger, IWrapper wrapper)
        {
            _logger = logger;
            _wrapper = wrapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserRequest userRequest)
        {
            if (ModelState.IsValid)
            {
                string token = string.Empty;
                bool isSucess = false;
                string message = string.Empty;
                (isSucess, message, token) = await _wrapper.GetToken(userRequest.UserName, userRequest.Password);
                if (isSucess)
                {
                    HttpContext.Session.SetString("Token", token);
                    HttpContext.Session.SetString("UserName", userRequest.UserName);
                    return RedirectToAction("Patient", "Home", new
                    {
                        sortBy = "Name",
                        query = "",
                        departmentId = 0,
                        startAdmissionDate = "",
                        endAdmissionDate = "",
                        page = 1,
                        pageSize = 10
                    });
                }
                ModelState.AddModelError("", message);
            }
            return View("Index");
        }
        [Route("Patient")]
        [HttpGet]
        public async Task<IActionResult> Patient(string sortBy = "Name", string query = "", int departmentId = 0, string startAdmissionDate = "", string endAdmissionDate = "", int page = 1, int pageSize = 10)
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            string queryq = String.Empty;
            if (query != "" && query != null)
            {
                queryq = query.Trim();
            }
            string startAdmissionDate1 = String.Empty;
            if (startAdmissionDate1 != "")
            { startAdmissionDate1 = startAdmissionDate1.Trim(); }
            string endAdmissionDate1 = String.Empty;
            if (endAdmissionDate1 != "") { endAdmissionDate1 = endAdmissionDate1.Trim(); }

            List<PatientDetails> patientDetails = new List<PatientDetails>();
            bool isSucess = false;
            string message = string.Empty;
            (isSucess, message, patientDetails) = await _wrapper.GetPatientDetils(queryq, departmentId, startAdmissionDate1, endAdmissionDate1, sortBy, page, pageSize, 1000);
            if (isSucess)
            {
                patientViewModel.PatientDetails = patientDetails;
            }
            patientViewModel.DepartmentList = GetAllDepartment(departmentId);
            patientViewModel.SortByList = GetSortBy(sortBy);
            patientViewModel.SortBy = sortBy;
            patientViewModel.Department = departmentId;
            patientViewModel.StartDate = startAdmissionDate1;
            patientViewModel.EndDate = endAdmissionDate1;
            patientViewModel.Search = query;


            return View("Patient", patientViewModel);
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
        private List<SelectListItem> GetAllDepartment(int departmentId)
        {

            Dictionary<int, string> departments = new Dictionary<int, string>();

            departments.Add(1, "Casualty department");
            departments.Add(2, "Operating theatre (OT)");
            departments.Add(3, "Intensive care unit (ICU)");
            departments.Add(4, "Anesthesiology department");
            departments.Add(5, "Cardiology department");
            departments.Add(6, "ENT department");
            departments.Add(7, "Geriatric department");
            departments.Add(8, "Gastroenterology department");
            departments.Add(9, "General surgery");
            departments.Add(10, "Gynaecology department");
            departments.Add(11, "Haematology department");
            departments.Add(12, "Pediatrics department");
            departments.Add(13, "Neurology department");
            departments.Add(14, "Oncology department");
            departments.Add(15, "Opthalmology department");
            departments.Add(16, "Orthopaedic department");
            departments.Add(17, "Urology department");
            departments.Add(18, "Psychiatry department");
            departments.Add(19, "Inpatient Department (IPD)");
            departments.Add(20, "Outpatient Department (OPD)");


            List<SelectListItem> department = new List<SelectListItem>();
            if (departmentId == 0)
            {
                department.Add(new SelectListItem() { Text = "Select", Value = "0", Selected = true });
            }
            else
            {
                department.Add(new SelectListItem() { Text = "Select", Value = "0" });
            }

            foreach (var item in departments)
            {
                if (departmentId == item.Key)
                {
                    department.Add(new SelectListItem() { Text = item.Value, Value = item.Key.ToString(), Selected = true });
                }
                else
                {
                    department.Add(new SelectListItem() { Text = item.Value, Value = item.Key.ToString() });
                }
            }

            return department;
        }

        private List<SelectListItem> GetSortBy(string sortby = "name")
        {

            Dictionary<string, string> departments = new Dictionary<string, string>();

            departments.Add("name", "Ptient Name");
            departments.Add("department", "Department");
            departments.Add("adminsiondate", "adminsiondate");



            List<SelectListItem> sortByList = new List<SelectListItem>();


            foreach (var item in departments)
            {
                if (sortby == item.Key)
                {
                    sortByList.Add(new SelectListItem() { Text = item.Value, Value = item.Key.ToString(), Selected = true });
                }
                else
                {
                    sortByList.Add(new SelectListItem() { Text = item.Value, Value = item.Key.ToString() });
                }
            }

            return sortByList;
        }
    }
}