using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class CreateProjectViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "назва обов'язкова")]
        public string Name { get; set; }

        [Required(ErrorMessage = "опис обов'язковий")]
        public string Description { get; set; }

        [Required(ErrorMessage = "короткий опис обов'язковий")]
        public string shortDescription { get; set; }

        public IList<SelectListItem>? Skills { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "технології обов'язкові")]
        [MaxLength(8, ErrorMessage = "забагато")]
        public IList<int> SelectedSkillsIds { get; set; }
    }
}
