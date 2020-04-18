using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreModels
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(100)]
        [DataType("varchar")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(75)]
        [DataType("varchar")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [InverseProperty(nameof(TimeRecord.User))]
        public virtual ICollection<TimeRecord> TimeRecords { get; set; }

    }
}
