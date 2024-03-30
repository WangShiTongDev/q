using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.Services
{
    public class SkillService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public SkillService(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public IList<SkillDTO> GetSkills()
        {
            var skills = dataContext.Skills.ToList();
            return mapper.Map<IList<SkillDTO>>(skills);
        }
    }
}
