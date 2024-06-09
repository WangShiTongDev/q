using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Filters;
using DevTeamUp.DAL.EF;
using DevTeamUp.DAL.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

        public UserDto? GetUser(int id)
        {

            // Может быть проблема при мапинге User to
            // UserDTO в части UserDTO.ProjectsMember -> ProjectDTO.Stack
            // Возможно слишком глубокий маппинг
            var user = _dataContext.Users
                .Include( u => u.Skills)
                .Include( u => u.ProjectsMember)
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
                return null;

            var dto = _mapper.Map<UserDto>(user);
            return dto;
        }

        public ProfilePageDTO Profile(int userId)
        {
            var user = _dataContext.Users
                .Include(u => u.Skills)
                .Include(u => u.CommentsForUser )
                .Include(u => u.ProjectsMember)
                    .ThenInclude(project => project.Stack)

                .FirstOrDefault(x => x.Id == userId);

            if (user == null) throw new ArgumentException("User not found");
            var profile = new ProfilePageDTO();
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.About = user.About;
            profile.Bio = user.Bio;
            profile.GitHubLink = user.GitHubLink;
            profile.Skills = user.Skills.Select(s => new SkillDTO
            {
                Id = s.Id,
                Name = s.Name,
            });

            profile.Projects = _mapper.Map<IEnumerable<ProjectDTO>>(user.ProjectsMember);
            profile.Reviews = _mapper.Map<IEnumerable<ReviewDTO>>(user.CommentsForUser);

            return profile;
        }

        public ProfileDTO SelfProfile(int userId)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException();

            return _mapper.Map<ProfileDTO>(user);
        }


        public UserDto ChangeSkills(int id, IList<int> skillsIds)
        {
            var user = _dataContext.Users.Include( u => u.Skills).First(x => x.Id == id);

            user.Skills.Clear();
            user.Skills = _dataContext.Skills.Where(s => skillsIds.Contains(s.Id)).ToList();
            
 
            _dataContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public bool IsProfileCompleted(int userId)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException();

            return user.IsProfileCompleted;
        }

        public void ProfileInit(ProfileDTO profile, int userId)
        {
            var currentUser = _dataContext.Users.First(u => userId == u.Id);

            currentUser.FirstName = profile.FirstName;
            currentUser.LastName = profile.LastName;
            currentUser.About = profile.About;
            currentUser.GitHubLink = profile.GitHubLink;
            currentUser.Bio = profile.Bio;
            //currentUser.Skills = 

            var skillIds = profile.Skills.Select(s => s.Id).ToList();
            var res = _dataContext.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToList();

           
                
            _ = res;
            currentUser.Skills.Clear();
            currentUser.Skills = res;
            currentUser.IsProfileCompleted = true;  
            _dataContext.SaveChanges();
            //currentUser.FirstName = profile.FirstName;
            //currentUser.LastName = profile.LastName;
            //currentUser.About = profile.About;
            //currentUser.Skills = _dataContext.Skills
            //    .Where(s => profile.TechnologiesIds.Contains(s.Id))
            //    .ToList();

            //currentUser.IsProfileCompleted = true;
            //_dataContext.SaveChanges();
        }

        public IList<ProfileListItemDTO > GetProfiles(ProfileFilter? filter)
        {
            var query = _dataContext.Users.Where(u => u.IsProfileCompleted);
            if(filter != null && !string.IsNullOrEmpty(filter?.Query))
            {
                query = query.Where(u => u.FirstName.ToLower().Contains(filter.Query) || 
                    u.LastName.ToLower().Contains(filter.Query) 
                );
            }

            if(filter?.Skills?.Any() == true)
            {
                query = query.Where(u => u.Skills.Any(s => filter.Skills.Contains(s.Id)));
            }

            var users = query.ToList();

            var profiles = _mapper.Map<IList<ProfileListItemDTO>>(users);

            return profiles;
        }
    }
}
