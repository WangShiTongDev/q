using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "User name is empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is empty")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
