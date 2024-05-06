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
        public string Name { get; set; }
        public string Description { get; set; }

        
        public IList<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        
        public IList<int> SelectedSkillsIds { get; set; }
    }
}
