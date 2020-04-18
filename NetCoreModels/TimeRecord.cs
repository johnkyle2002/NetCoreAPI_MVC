using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreModels
{
    public class TimeRecord
    {
        public int TimeRecordID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual Employee User { get; set; }
    }
}
