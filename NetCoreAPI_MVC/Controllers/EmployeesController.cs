using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreInterface;
using NetCoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    { 
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _employeeService.GetAll();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            if(await _employeeService.Update(employee))
            {
                return NoContent();
            }
            return NotFound(); 
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _employeeService.Create(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

           await  _employeeService.Delete(employee);

            return employee;
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await _employeeService.Exists(e=> e.EmployeeID == id);
        }
    }
}
