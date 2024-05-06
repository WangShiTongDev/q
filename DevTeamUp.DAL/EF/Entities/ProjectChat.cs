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
        public virtual Project Project { get; set; }

        public virtual IList<Message> Messages { get; set; }

    }
}
