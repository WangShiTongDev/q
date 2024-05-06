using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class ProfilePageDTO
    {
        //public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public IEnumerable<SkillDTO> Skills { get; set; }
        public IEnumerable<ProjectDTO> Projects { get; set; }
        public IEnumerable<ReviewDTO> Reviews { get; set; }

    }
}
