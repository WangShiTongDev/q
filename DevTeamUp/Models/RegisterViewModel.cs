using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User name is empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is empty")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password not compare")]
        //public string ConfirmPassword { get; set; }
    }
}
