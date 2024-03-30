using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Filters;
using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevTeamUp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService projectService;
        private readonly SkillService skillService;

        
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public ProjectController(ProjectService projectService, UserManager<User> userManager, IMapper mapper, SkillService skillService)
        {
            this.projectService = projectService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.skillService = skillService;
        }


        public IActionResult Index()
        {
            return View();
        }

        // view projects list
        public IActionResult List(ProjectsFilter filter, int page = 1)
        {
            _ = filter;
            ProjectsListDTO projectsListDTO = projectService.GetPage(page, filter);
            ProjectsListViewModel model = new()
            {
                Count = projectsListDTO.Projects.Count,
                TotalCount = projectsListDTO.TotalCount,
                TotalPages = projectsListDTO.TotalPages,
                Projects = projectsListDTO.Projects.Select(dto => new ProjectViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    OwnerId = dto.OwnerId,
                    Stack = dto.Stack.Select(skill => new SkillViewModel { 
                        Id = skill.Id,
                        Name = skill.Name,
                    })
                    .ToList()

                }).ToList()
            };

            ViewBag.AvailableTechnologies = listItemsAvailableTechnologies();
            return View(model);
        }

        public IActionResult CreateProject()
        {
            CreateProjectViewModel model = new()
            {
                AvailableTechnologies = listItemsAvailableTechnologies()
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = int.Parse(userManager.GetUserId(this.User));
                var projectDto = new CreatedProjectDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    TechnologyIds = model.SelectedTechnologyIds
                };

                var newProject = projectService.CreateProject(projectDto, userId);

                model.Name = newProject.Name;
                model.Description = newProject.Description;


                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult JoinToProject(int projectId)
        {
            throw new NotImplementedException();
        }

        private IList<SelectListItem> listItemsAvailableTechnologies()
        {
            var availableTechnologies = skillService
                .GetSkills()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            return availableTechnologies;
        }
    }
}
