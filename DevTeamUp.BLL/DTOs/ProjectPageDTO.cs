using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class ProjectPageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }


        public ProfileDTO OwnerProfile { get; set; }
        public IList<ProfileDTO> Members { get; set; }

        public IList<SkillDTO> Stack { get; set; }
    }
}
