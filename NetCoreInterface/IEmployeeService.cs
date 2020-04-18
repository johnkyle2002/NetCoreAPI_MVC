using NetCoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreInterface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
    }
}