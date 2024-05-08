using DevTeamUp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models.Profile
{
    public class ProfilePageViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string? GitHubLink { get; set; }
        public string Bio { get; set; }

        public bool IsAuthor { get; set; }

        public IEnumerable<SkillViewModel> Skills { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }
}
