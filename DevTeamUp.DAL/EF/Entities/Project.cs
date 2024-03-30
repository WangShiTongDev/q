using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }

        [InverseProperty(nameof(User.ProjectsOwner))]
        public User Owner { get; set; }

        [InverseProperty(nameof(User.ProjectsMember))]
        public IList<User> Members { get; set; }

        public IList<Skill> Stack { get; set; }
    }
}
