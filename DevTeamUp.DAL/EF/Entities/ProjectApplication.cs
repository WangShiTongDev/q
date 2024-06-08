using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{

    public enum ProjectApplicationStatus
    {
        pending,
        accepted,
        rejected
    }

    public class ProjectApplication : EntityBase
    {
        public string Message { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public ProjectApplicationStatus Status { get; set; }
    }
}
