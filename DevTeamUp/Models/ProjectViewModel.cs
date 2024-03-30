using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class ProjectViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is empty")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }

        public IList<SkillViewModel> Stack { get; set; }
    }
}
