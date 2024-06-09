using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Filters;
using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF;
using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Models;
using DevTeamUp.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly DataContext dataContext;
        //private UserDto _currentUser;
        private UserDto? currentUser
        {
            get
            {
                return userService.GetUser(int.Parse(userManager.GetUserId(User)));
            }
        }
        public ProfileController(ProjectService projectService, UserManager<User> userManager, IMapper mapper, UserService userService, SkillService skillService, DataContext dataContext)
        {
            this.projectService = projectService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
            this.skillService = skillService;
            this.dataContext = dataContext;
        }

        public IActionResult Index(int? uId)
        {
            if(uId == null || uId == currentUser.Id)
            {
                var userId = int.Parse(userManager.GetUserId(User));
                var userProfile = userService.Profile(userId);
                userProfile.IsProfileOwner = true;
                
                ViewBag.MembershipApplications = projectService.MembershipApplications(userId); 

                return View(userProfile);
            }
            else
            {
                var userProfile = userService.Profile(uId.Value);
                userProfile.Id = uId.Value;
                var userEntity = dataContext.Users.First(u => u.Id == int.Parse(userManager.GetUserId(User)));
                ViewBag.isCollaborator = userEntity.ProjectsMember.Any(p => p.Members.Any(m => m.Id == userProfile.Id));
                
                return View(userProfile);
            }

            //var userDto = userService.GetUser(userId);


            //var profileModel = new ProfileViewModel
            //{
            //    Username = userDto.Username,
            //};
            //profileModel.Skills = profileModel.toSelectListItem(userDto.Skill);
            //var availableSkills = skillService.GetSkills().ExceptBy(userDto.Skill.Select( i=> i.Id), u => u.Id );
            //profileModel.AvailableSkills = profileModel.toSelectListItem(availableSkills);



         
        }

        //[HttpGet("[controller]/{id}", Order = int.MaxValue)]
        //public IActionResult UserProfile(int id)
        //{
        //    if(currentUser.Id == id)
        //        return RedirectToAction("Index");
        //    // мб делать проверку на собственную страницу 
        //    return View();
        //}

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
   
            if(!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                model.AvailableSkills = skillService.GetSkills().Select(s =>
                         new SelectListItem(s.Name, s.Id.ToString()))
                     .ToList();
                return View(model);
            }

            var dto = mapper.Map<ProfileDTO>(model);
            userService.ProfileInit(dto, currentUser.Id);

            return RedirectToAction("Index");

        }

        public IActionResult List(ProfileFilter? filter)
        {
            _ = filter;
            ViewBag.Skills = skillService.GetSkills().Select(s =>
                         new SelectListItem(s.Name, s.Id.ToString()))
                     .ToList();
            var profiles = userService.GetProfiles(filter);
            
            return View(profiles);
        }

        public IActionResult JoinResult(int reqId, bool status)
        {
          
            projectService.JoinResult(reqId, status);

            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult CreateReview(ReviewViewModel model)
        {

            Review newReview = new Review()
            {
                AuthorId = currentUser.Id,
                Description = model.Description,
                RecipientId = model.ProfileId,
                CreatedAt = DateTime.Now,
            };

            dataContext.Reviews.Add(newReview);
            dataContext.SaveChanges();

            return Redirect($"Index?uId={newReview.RecipientId}");
        }
    }
}
