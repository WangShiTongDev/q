using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    // use for profile init
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string? GitHubLink { get; set; }
        public string? Bio { get; set; }

        public IList<SkillDTO> Skills { get; set; }

    }
}
