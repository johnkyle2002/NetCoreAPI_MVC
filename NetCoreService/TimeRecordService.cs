using Microsoft.EntityFrameworkCore;
using NetCoreInterface;
using NetCoreModels;
using NetCoreRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NetCoreModels.ViewModel;

namespace NetCoreService
{
    public class TimeRecordService : ServiceModel<TimeRecord>, ITimeRecordService
    {
        public TimeRecordService(NetCoreDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TimeRecordViewModel>> GetAllWithUser()
        {
            return await this.Entity.Include(i => i.User)
                .Select(s => new TimeRecordViewModel
                {
                    StartDateTime = s.StartDateTime,
                    EmployeeName = s.User.FullName,
                    EndDateTime = s.EndDateTime,
                    TimeRecordID = s.TimeRecordID
                })
                .ToListAsync();
        }
    }
}
