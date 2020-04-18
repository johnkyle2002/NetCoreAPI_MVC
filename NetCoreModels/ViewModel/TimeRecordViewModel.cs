using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCoreModels.ViewModel
{
    public class TimeRecordViewModel
    {
        public int TimeRecordID { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}")]
        public DateTime StartDateTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}")]
        [Display(Name = "End Date")]
        public DateTime EndDateTime { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}
