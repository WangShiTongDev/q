using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public class Skill : EntityBase
    {
        public string Name { get; set; }
        
        public IList<User> Users { get; set; }
        public IList<Project> Projects { get; set; }
    }
}
