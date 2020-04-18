using NetCoreInterface;
using NetCoreModels;
using NetCoreRepository;

namespace NetCoreService
{
    public class TimeRecordService : ServiceModel<TimeRecord>, ITimeRecordService
    {
        public TimeRecordService(NetCoreDBContext context) : base(context)
        { 
        }
    }
}
