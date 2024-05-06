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
        
        public virtual IList<User> Users { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
