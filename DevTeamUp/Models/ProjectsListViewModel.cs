using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class ProjectsListViewModel
    {
        public IList<ProjectViewModel> Projects { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; } 

        public ProjectsFilter Filter { get; set; }
    }
}
