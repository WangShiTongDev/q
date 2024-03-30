using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class CreatedProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<int> TechnologyIds { get; set; }
    }
}
