using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class ProfileInitVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public IEnumerable<int> TechnologiesIds { get; set; }
    }
}
