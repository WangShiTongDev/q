﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int OwnerId { get; set; }
        
        public IList<SkillDTO> Stack { get; set; }
    }
}