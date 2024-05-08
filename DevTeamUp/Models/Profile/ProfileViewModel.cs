using DevTeamUp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }

        public IList<SkillViewModel> Skills { get; set; }
    }
}
