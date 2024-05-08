using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DevTeamUp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProjectService projectService;
        private readonly SkillService skillService;
        private readonly UserService userService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        //private UserDto _currentUser;
        private UserDto? currentUser
        {
            get
            {
                return userService.GetUser(int.Parse(userManager.GetUserId(User)));
            }
        }
        public ProfileController(ProjectService projectService, UserManager<User> userManager, IMapper mapper, UserService userService, SkillService skillService)
        {
            this.projectService = projectService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
            this.skillService = skillService;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(userManager.GetUserId(User));
            //var userDto = userService.GetUser(userId);


            //var profileModel = new ProfileViewModel
            //{
            //    Username = userDto.Username,
            //};
            //profileModel.Skills = profileModel.toSelectListItem(userDto.Skill);
            //var availableSkills = skillService.GetSkills().ExceptBy(userDto.Skill.Select( i=> i.Id), u => u.Id );
            //profileModel.AvailableSkills = profileModel.toSelectListItem(availableSkills);


            var userProfile = userService.Profile(userId);

            _ = userProfile;
            
            return View(userProfile);
        }

        [HttpGet("[controller]/{id}", Order = int.MaxValue)]
        public IActionResult UserProfile(int id)
        {
            if(currentUser.Id == id)
                return RedirectToAction("Index");
            // мб делать проверку на собственную страницу 
            return View();
        }

        public IActionResult ProfileInit()
        {
          
            var profile = userService.SelfProfile(currentUser.Id);
            ProfileInitVM model = mapper.Map<ProfileInitVM>(profile);

            model.AvailableSkills = skillService.GetSkills().Select(s =>
                    new SelectListItem(s.Name, s.Id.ToString()))
                .ToList();


            _ = model;

            return View(model);
        }

        [HttpPost]
        public IActionResult ProfileInit(ProfileInitVM model)
        {
            _ = model;
            model.AvailableSkills = skillService.GetSkills().Select(s =>
                     new SelectListItem(s.Name, s.Id.ToString()))
                 .ToList();
            var dto = mapper.Map<ProfileDTO>(model);
            userService.ProfileInit(dto, currentUser.Id);
            return View(model);

        }

        public IActionResult List()
        {
            var profiles = userService.GetProfiles();
            _ = profiles;
            return View(profiles);
        }
    }
}
