using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public class User : IdentityUser<int>, IEntityBase
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? About { get; set; }
        public bool IsProfileCompleted { get; set; }
        public string? GitHubLink { get; set; }
        public string? Bio { get; set; }

        public virtual IList<Skill> Skills { get; set; }
        public virtual IList<Review> UserComments { get; set; } 

        public virtual IList<Review> CommentsForUser { get; set; } 



        [InverseProperty(nameof(Project.Members))]
        public virtual IList<Project> ProjectsMember { get; set; }

        [InverseProperty(nameof(Project.Owner))]
        public virtual IList<Project> ProjectsOwner { get; set; }

        [InverseProperty(nameof(ProjectApplication.Author))]
        public virtual IList<ProjectApplication> ProjectApplications { get; set; }
    }
}
