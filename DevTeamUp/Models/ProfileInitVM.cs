using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models
{
    public class ProfileInitVM
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? About { get; set; }
        public string? GitHubLink { get; set; }
        public string? Bio { get; set; }
        public IEnumerable<SelectListItem> AvailableSkills { get; set; }
        public IEnumerable<int> SelectedSkills { get; set; }

    }
}
