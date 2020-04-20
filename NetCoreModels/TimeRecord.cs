using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreModels
{
    public class TimeRecord
    {
        public int TimeRecordID { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}")]
        public DateTime StartDateTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}")]
        [Display(Name ="End Date")]
        public DateTime EndDateTime { get; set; }
        [Display(Name = "Employee")]
        public int UserID { get; set; }
        [Display(Name ="Employee")]
        [ForeignKey(nameof(UserID))]
        public virtual Employee User { get; set; }
    }
}
