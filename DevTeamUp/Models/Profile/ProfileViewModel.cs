using DevTeamUp.BLL.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Models.Profile
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        

        public IList<SelectListItem> AvailableSkills { get; set; }
        public IList<SelectListItem> Skills { get; set; }
        public IList<int> SelectSkills { get; set; }

        public IList<SelectListItem> toSelectListItem(IEnumerable<SkillDTO> list)
        {
            
            var selectListItems = list.Select(s => new SelectListItem 
            { 
                Text = s.Name, 
                Value = s.Id.ToString() 
            }).ToList();
            return selectListItems ?? new List<SelectListItem>();
        }
    }
}
