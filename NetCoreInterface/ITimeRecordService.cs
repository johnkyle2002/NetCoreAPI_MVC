using NetCoreModels;
using NetCoreModels.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreInterface
{
    public interface ITimeRecordService : IServiceModel<TimeRecord>
    {
        Task<IEnumerable<TimeRecordViewModel>> GetAllWithUser();
    }
}