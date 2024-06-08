using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public enum ProjectStatus
    {
        Closed,
        Private,
        Open
    }

    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string shortDescription { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Open;

        public int OwnerId { get; set; }

        [InverseProperty(nameof(User.ProjectsOwner))]
        public virtual User Owner { get; set; }

        [InverseProperty(nameof(User.ProjectsMember))]
        public virtual IList<User> Members { get; set; }

        public virtual IList<Skill> Stack { get; set; }

        [InverseProperty(nameof(ProjectApplication.Project))]
        public virtual IList<ProjectApplication> ProjectApplications { get; set; }
    }
}
