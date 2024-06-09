using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class UpdateProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string shortDescription { get; set; }
        public string Description { get; set; }
        public IList<int> Stack { get; set; }
    }
}
