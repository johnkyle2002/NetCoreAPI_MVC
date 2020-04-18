using Microsoft.EntityFrameworkCore;
using NetCoreInterface;
using NetCoreModels;
using NetCoreRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly NetCoreDBContext _context;

        public EmployeeService(NetCoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employee.ToListAsync();
        }

    }
}
