using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreInterface;
using NetCoreModels;
using NetCoreModels.ViewModel;
using NetCoreMVC.Commons.Helper;

namespace NetCoreMVC.Controllers
{
 
    [Authorize]
    public class TimeRecordController : Controller
    {
        private readonly IClientHelper _clientHelper;

        public TimeRecordController(IClientHelper clientHelper)
        {
            _clientHelper = clientHelper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _clientHelper.GetClient<IEnumerable<TimeRecordViewModel>>("/api/TimeRecords", null, ClaimsHelper.GetJwtToken(User));

            if (result == null)
            {
                ModelState.AddModelError("error", "Unable to retrieve record.");
                return View(new List<TimeRecord>());
            }

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result.Entity = result.Entity.OrderBy(o => o.StartDateTime).ToList();
                return View(result.Entity);
            }
            else
            {
                return View(new List<TimeRecord>());
            }
        }

        public async Task<IActionResult> Create()
        {
            var result = await _clientHelper.GetClient<IEnumerable<Employee>>("/api/Employees", null, ClaimsHelper.GetJwtToken(User));

            ViewData["UserId"] = new SelectList(result.Entity.ToList(), "EmployeeID", "FullName");
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(TimeRecord timeRecort)
        {
            if (!ModelState.IsValid)
                return View(timeRecort);

            var result = await _clientHelper.PostClient("api/TimeRecords", timeRecort, ClaimsHelper.GetJwtToken(User));

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            return View(timeRecort);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _clientHelper.GetClient<IEnumerable<Employee>>("/api/Employees", null, ClaimsHelper.GetJwtToken(User));

            ViewData["UserId"] = new SelectList(emp.Entity.ToList(), "EmployeeID", "FullName");

            var result = await _clientHelper.GetClient<TimeRecord>("api/TimeRecords", "/" + id, ClaimsHelper.GetJwtToken(User));

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View(result.Entity);
            }
            else
            {
                return View(new TimeRecord());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TimeRecord timeRecord)
        {
            if (!ModelState.IsValid)
                return View(timeRecord);

            var result = await _clientHelper.PutClient<TimeRecord>($"api/TimeRecords/{timeRecord.TimeRecordID}", timeRecord, ClaimsHelper.GetJwtToken(User));

            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(timeRecord);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clientHelper.DeleteClient<TimeRecord>($"api/TimeRecords/{id}", ClaimsHelper.GetJwtToken(User));

            if (result != null)
            {
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
        }
    }
}