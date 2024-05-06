using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Models;
using DevTeamUp.Models.Profile;
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

        private UserDto _currentUser;
        private UserDto currentUser
        {
            get
            {
                return _currentUser ??
                    userService.GetUser(int.Parse(userManager.GetUserId(User)));
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

       

        [HttpGet("user/{id}")]
        public IActionResult UserProfile(int id)
        {
            // мб делать проверку на собственную страницу 
            return View();
        }

        public IActionResult ProfileInit()
        {
            ViewBag.availableSkills = skillService.GetSkills();
            return View();
        }

        [HttpPost]
        public IActionResult ProfileInit(ProfileInitVM model)
        {
            _ = model;
            var dto = new ProfileDTO
            {
                UserId = currentUser.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                About = model.About,
                TechnologiesIds = model.TechnologiesIds,
            };
            userService.ProfileInit(dto);
            var completedUser = userService.GetUser(dto.UserId);

            _ = completedUser;

            ViewBag.availableSkills = skillService.GetSkills();
            return View();
            
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
