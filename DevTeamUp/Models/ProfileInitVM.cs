using DevTeamUp.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class ProfileInitVM
    {
        [Required(ErrorMessage = "ім'я обов'язкове")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "прізвище обов'язкове")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "поле про себе обов'язкове")]
        public string About { get; set; }
        [Required(ErrorMessage = "посилання на гіт хаб обов'язкове")]
        [GitHubUrl(ErrorMessage = "не валідне посилання")]
        public string GitHubLink { get; set; }
        [Required(ErrorMessage = "короткий опис обов'язковий")]
        public string Bio { get; set; }
        
        public IEnumerable<SelectListItem>? AvailableSkills { get; set; }
        [Required(ErrorMessage = "навики обов'язкові ")]
        [MaxLength(8, ErrorMessage = "забагато")]
        public IEnumerable<int> SelectedSkills { get; set; }

    }
}
