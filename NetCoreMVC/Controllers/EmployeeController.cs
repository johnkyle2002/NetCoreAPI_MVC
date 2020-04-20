using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreModels;
using NetCoreModels.ViewModel;
using NetCoreMVC.Commons.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IClientHelper _clientHelper;

        public EmployeeController(IClientHelper clientHelper)
        {
            _clientHelper = clientHelper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _clientHelper.GetClient<IEnumerable<Employee>>("/api/Employees", null, ClaimsHelper.GetJwtToken(User));

            if (result == null)
            {
                ModelState.AddModelError("error", "Unable to retrieve record.");
                return View(new List<Employee>());
            }

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {                
                return View(result.Entity);
            }
            else
            {
                return View(new List<Employee>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var result = await _clientHelper.PostClient("api/Employees", employee, ClaimsHelper.GetJwtToken(User));

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _clientHelper.GetClient<Employee>("api/Employees", "/" + id, ClaimsHelper.GetJwtToken(User));

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View(result.Entity);
            }
            else
            {
                return View(new Employee());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var result = await _clientHelper.PutClient<Employee>($"api/Employees/{employee.EmployeeID}", employee, ClaimsHelper.GetJwtToken(User));

            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(employee);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clientHelper.DeleteClient<Employee>($"api/Employees/{id}", ClaimsHelper.GetJwtToken(User));

            if (result != null)
            {
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _clientHelper.GetClient<NetCoreModels.Employee>($"/api/Employees/{id}", null, ClaimsHelper.GetJwtToken(User));

            if (employee == null)
                return NotFound();

            var result = await _clientHelper.GetClient<IEnumerable<TimeRecord>>($"/api/TimeRecords/EmployeeTimeRecord/{id}",null, ClaimsHelper.GetJwtToken(User));

            var model = new EmployeeDetailsViewModel
            {
                Employee = employee.Entity,
                TimeRecords = result.Entity.OrderByDescending(o=> o.StartDateTime).ToList()
            };

            return View(model);
        }
    }
}