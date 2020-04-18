using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVC.Models.ViewModel
{
    public class UserLoginViewModel
    {
        [Required] 
        public string Password { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Username { get; set; }
    }
}
