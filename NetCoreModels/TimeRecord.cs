using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreModels
{
    public class TimeRecord
    {
        public int TimeRecordID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual ICollection<User> Users { get; set; }
    }
}
