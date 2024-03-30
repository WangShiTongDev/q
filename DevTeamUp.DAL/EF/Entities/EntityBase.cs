using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public interface IEntityBase
    {
        int Id { get; set; }
    }
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
    }
}
