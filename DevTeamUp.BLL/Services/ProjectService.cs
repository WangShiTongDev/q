using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Filters;
using DevTeamUp.DAL.EF;
using DevTeamUp.DAL.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.Services
{
    public class ProjectService
    {
        private const int pageSize = 5;

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProjectService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public ProjectDTO CreateProject(CreatedProjectDTO createdProjectDTO, int userId)
        {
            var user = _dataContext.Users.First(u => u.Id == userId);
            var eProject = _mapper.Map<Project>(createdProjectDTO);
            eProject.OwnerId = userId;
            eProject.Stack = _dataContext.Skills
                .Where(s => createdProjectDTO.SkillsIds.Contains(s.Id))
                .ToList();
            
            _dataContext.Projects.Add(eProject);
            eProject.Members = new List<User>(new[] { user });
            _dataContext.SaveChanges();


            var createdProject = _dataContext.Projects
                .Include(p => p.Stack)
                .Include(p => p.Owner)
                .First(p => p.Id == eProject.Id);
            
            var projectDTO = _mapper.Map<ProjectDTO>(createdProject);
            return projectDTO;
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            //return _dataContext.Projects.Select(p => new ProjectDTO {
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    OwnerId = p.OwnerId,
            //}).ToList();    

            return _mapper.Map<IList<ProjectDTO>>(_dataContext.Projects.Include(p => p.Stack).ToList());

        }
        public IEnumerable<ProjectDTO> OwnersProjects(int userId)
        {

            var projects = _dataContext.Projects
                .Include(p => p.Stack)
                .Where(p => p.OwnerId == userId).ToList();
            return _mapper.Map<IList<ProjectDTO>>(projects);

        }

        public ProjectsListDTO GetPage(int pageIndex, ProjectsFilter? filter)
        {
            // Пример того, как должно работать. SQL запрос работает отлично
            //  select*
            //  from Projects as p, ProjectSkill as ps
            //  where p.id = ps.ProjectsId
            //      and ps.StackId = (select Id from Skills where Skills.[Name] = N'C#/.NET')
	        //      and p.[Name] like '%dima1%'

            var queryExpression = _dataContext.Projects
                .Include(p => p.Stack)
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(filter?.Keyword))
                queryExpression = queryExpression.Where(p => p.Name.Contains(filter.Keyword));

            // Выбираем все проекты, если у них есть хотя бы 1 технология из фильтра
            if (filter?.TechnologyIds != null && filter.TechnologyIds.Any())
                queryExpression = queryExpression.Where(p => p.Stack.Any(t => filter.TechnologyIds.Contains(t.Id)));

            // Хахахаха, у меня просто в фильтры не заполнялся Keyword
            //if (!String.IsNullOrWhiteSpace(filter?.Keyword))
            //{
            //    queryExpression = queryExpression.Where(p => p.Name.Contains(filter.Keyword));
            //    if (filter?.TechnologyIds != null && filter.TechnologyIds.Any())
            //    {
            //        queryExpression = queryExpression.Where(p => p.Stack.Any(t => filter.TechnologyIds.Contains(t.Id)));
            //    }
            //}
            //else if (filter?.TechnologyIds != null && filter.TechnologyIds.Any())
            //{
            //    queryExpression = queryExpression.Where(p => p.Stack.Any(t => filter.TechnologyIds.Contains(t.Id)));
            //}

            var projectsPage = queryExpression.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            var result = _mapper.Map<IList<ProjectDTO>>(projectsPage);

            // TODO: add total items, totalPages and pageSize
            var totalCount = _dataContext.Projects.Count();
            return new ProjectsListDTO
            {
                Projects = result,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public ProjectDTO JoinToProject(int projectId, int userId)
        {
            var project = _dataContext.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
                throw new ArgumentException();

            project.Members.Add(_dataContext.Users.First(u => u.Id == userId));
            _dataContext.SaveChanges();
            return _mapper.Map<ProjectDTO>(project);
        }

        public IEnumerable<ProjectDTO> GetProjectByUser(int userId)
        {
            var user = _dataContext.Users
                .Include(u => u.ProjectsMember)
                .FirstOrDefault(u => u.Id == userId);
            if(user == null)
                throw new ArgumentException("User not found");

            var projects = _mapper.Map<IEnumerable<ProjectDTO>>(user.ProjectsMember);

            return projects;
        }

        public bool Delete(int projectId)
        {

            // Предусмотреть все возможные кейсы, с учетом других юзеров на проекте, чатов и т.д.
            throw new NotImplementedException();
        }


    }
}
