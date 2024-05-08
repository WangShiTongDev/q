using DevTeamUp.BLL.DTOs;
using DevTeamUp.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class ProjectPageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public bool IsOwner = false;
        public bool IsMember = false;

        public ProfileViewModel OwnerProfile { get; set; }
        public IList<ProfileViewModel> Members { get; set; }

        public IList<SkillViewModel> Stack { get; set; }
    }
}
