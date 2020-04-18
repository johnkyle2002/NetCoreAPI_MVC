using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetCoreCommons.Extensions;
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
        public string FullName => $"{FirstName.ToTitleCase()} {LastName.ToTitleCase()}";

        [InverseProperty(nameof(TimeRecord.User))]
        public virtual ICollection<TimeRecord> TimeRecords { get; set; }


    }
}
