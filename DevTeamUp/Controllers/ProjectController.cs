using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Filters;
using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

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


        [HttpGet("[controller]/{id}", Order = int.MaxValue, Name = "def")]
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
            return RedirectToRoute("def", new { Id = 11});
        }

        public IActionResult List(ProjectsFilter? filter)
        {
            _ = filter;
            var projects = projectService.GetAllProjects(filter);

            ViewBag.Technologies = listItemsAvailableTechnologies();
            return View(projects);
        }

        public IActionResult CreateProject(int? projectId)
        {

            CreateProjectViewModel model = new()
            {
                Skills = listItemsAvailableTechnologies()
            };

            if(projectId == null)
            {

                return View(model);
            }
            else
            {
                var project = projectService.GetProject(projectId.Value);
                model.Name = project.Name;
                model.Description = project.Description;
                model.shortDescription = project.ShortDescription;
                model.Id = project.Id;
                return View(model);
            }
            
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            if(ModelState.IsValid)
            {
                // проверка на владельца
                if(model.Id != null)
                {
                    UpdateProjectDTO p = new()
                    {
                        Id = model.Id.Value,
                        Name = model.Name,
                        Description = model.Description,
                        shortDescription = model.shortDescription,
                        Stack = model.SelectedSkillsIds

                    };
                    
                    projectService.Update(p);
                    return RedirectToRoute("def", new { Id = p.Id });

                }
                else
                {
                    var userId = int.Parse(userManager.GetUserId(this.User));
                    var dto = mapper.Map<CreatedProjectDTO>(model);
                    _ = dto;
                    var newProject = projectService.CreateProject(dto, userId);
                    _ = newProject;
                    return RedirectToRoute("def", new { Id = newProject.Id });
                }


            }
            model.Skills = listItemsAvailableTechnologies();
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
