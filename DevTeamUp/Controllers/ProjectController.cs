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

        [HttpGet("[controller]/{id}", Order = int.MaxValue)]
        public IActionResult Index(int id)
        {
            try
            {

                var dto = projectService.GetProject(id);
                var model= mapper.Map<ProjectPageViewModel>(dto);
                _ = model;


                var userId = int.Parse(userManager.GetUserId(this.User));
                if (userId == model.OwnerProfile.Id)
                {
                    model.IsOwner = true;
                    model.IsMember = true;
                }
                else if(model.Members.Any(u => u.Id == userId))
                {
                    model.IsMember= true;
                }
                return View(model);
            }
            catch (Exception)
            {

                return Ok("Project not found");
                
            }

        }

        public IActionResult Test()
        {
            return RedirectToRoute("[controller]", new { id = 11 });
        }

        public IActionResult List()
        {
            //_ = filter;
            //ProjectsListDTO projectsListDTO = projectService.GetPage(page, filter);
            //ProjectsListViewModel model = new()
            //{
            //    Count = projectsListDTO.Projects.Count,
            //    TotalCount = projectsListDTO.TotalCount,
            //    TotalPages = projectsListDTO.TotalPages,
            //    Projects = projectsListDTO.Projects.Select(dto => new ProjectViewModel
            //    {
            //        Id = dto.Id,
            //        Name = dto.Name,
            //        Description = dto.Description,
            //        OwnerId = dto.OwnerId,
            //        Stack = dto.Stack.Select(skill => new SkillViewModel { 
            //            Id = skill.Id,
            //            Name = skill.Name,
            //        })
            //        .ToList()

            //    }).ToList()
            //};

            //ViewBag.AvailableTechnologies = listItemsAvailableTechnologies();
            //return View(model);


            var projects = projectService.GetAllProjects();

            
            return View(projects);
        }

        public IActionResult CreateProject()
        {
            CreateProjectViewModel model = new()
            {
                Skills = listItemsAvailableTechnologies()
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = int.Parse(userManager.GetUserId(this.User));
                //var projectDto = new CreatedProjectDTO
                //{
                //    Name = model.Name,
                //    Description = model.Description,
                //    SkillsIds = model.SelectedSkillsIds
                //};

                var dto = mapper.Map<CreatedProjectDTO>(model);
                _ = dto;
                var newProject = projectService.CreateProject(dto, userId);
                _ = newProject;
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult JoinToProject(RequestJoinViewModel model)
        {
            try
            {
                _ = model;
                var userId = int.Parse(userManager.GetUserId(this.User));
                projectService.JoinToProject(model.ProjectId, userId, model.Message);


                return RedirectToAction("Index", "Profile");
                //return RedirectToRoute("[controller]", new { id = project.Id });
            }
            catch (Exception)
            {

                
            }

            return Ok("error");
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

        public IActionResult Leave(int projectId)
        {
            var userId = int.Parse(userManager.GetUserId(this.User));
            projectService.LeaveProject(userId, projectId);
            return RedirectToAction("List");
        }
    }
}
