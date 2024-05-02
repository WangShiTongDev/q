using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class ProfileDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public IEnumerable<int> TechnologiesIds { get; set; }

    }
}
