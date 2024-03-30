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
        
        public IList<Skill> Skill { get; set; }
        public List<Review> UserComments { get; set; } 

        public List<Review> CommentsForUser { get; set; } 



        [InverseProperty(nameof(Project.Members))]
        public IList<Project> ProjectsMember { get; set; }

        [InverseProperty(nameof(Project.Owner))]
        public IList<Project> ProjectsOwner { get; set; }

    }
}
