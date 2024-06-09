using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.Filters
{
    public class ProfileFilter
    {
        public string? Query { get; set; }
        public IList<int>? Skills { get; set; }
    }
}
