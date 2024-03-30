using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.DAL.EF;
using DevTeamUp.DAL.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.Services
{
    public class UserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public UserDto GetUser(int id)
        {

            // Может быть проблема при мапинге User to
            // UserDTO в части UserDTO.ProjectsMember -> ProjectDTO.Stack
            // Возможно слишком глубокий маппинг
            var user = _dataContext.Users
                .Include( u => u.Skill)
                .Include( u => u.ProjectsMember)
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
                throw new ArgumentException();

            var dto = _mapper.Map<UserDto>(user);
            return dto;
        }

        public UserDto ChangeSkills(int id, IList<int> skillsIds)
        {
            var user = _dataContext.Users.Include( u => u.Skill).First(x => x.Id == id);

            user.Skill.Clear();
            user.Skill = _dataContext.Skills.Where(s => skillsIds.Contains(s.Id)).ToList();
            
 
            _dataContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }
    }
}
