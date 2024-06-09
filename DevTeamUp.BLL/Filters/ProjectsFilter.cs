using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.Filters
{
    public class ProjectsFilter
    {
        
        public string? Keyword { get; set; }
        public IList<int>? Technologies { get; set; }
    }
}
