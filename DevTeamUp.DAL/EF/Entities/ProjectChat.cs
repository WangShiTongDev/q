using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public class ProjectChat : EntityBase
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public IList<Message> Messages { get; set; }

    }
}
