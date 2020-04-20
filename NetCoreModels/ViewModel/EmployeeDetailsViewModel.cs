using System.Collections.Generic;

namespace NetCoreModels.ViewModel
{
    public class EmployeeDetailsViewModel
    { 
        public Employee Employee { get; set; }
        public IEnumerable<TimeRecord> TimeRecords { get; set; }
    }
}
