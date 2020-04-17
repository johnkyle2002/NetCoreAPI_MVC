using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreModels
{
    public class User
    {
        public int UserID { get; set; }

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

    }
}
