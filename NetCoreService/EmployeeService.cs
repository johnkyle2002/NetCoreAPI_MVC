using NetCoreInterface;
using NetCoreModels;
using NetCoreRepository;

namespace NetCoreService
{
    public class EmployeeService : ServiceModel<Employee>, IEmployeeService
    {
        public EmployeeService(NetCoreDBContext context) : base(context)
        {
        }
    }
}
