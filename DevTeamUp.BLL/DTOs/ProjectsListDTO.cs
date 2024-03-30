using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class ProjectsListDTO
    {
        public IList<ProjectDTO> Projects { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
