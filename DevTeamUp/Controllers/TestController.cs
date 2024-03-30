using DevTeamUp.BLL.DTOs;
using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevTeamUp.Controllers
{
    public class TestController : Controller
    {
        private readonly ProjectService projectService;
        private readonly UserManager<User> userManager;

        public TestController(ProjectService projectService, UserManager<User> userManager)
        {
            this.projectService = projectService;
            this.userManager = userManager;
        }


        public IActionResult CreateProject()
        {

            _ = this.User;
            var userId = int.Parse(userManager.GetUserId(this.User));

            var projectDTO = new CreatedProjectDTO
            {
                Name = "Test",
                Description = "Testssss",
            };

            projectService.CreateProject(projectDTO, userId);
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(CreatedProjectDTO projectDTO)
        {
            var userId = int.Parse( userManager.GetUserId(this.User));
            projectService.CreateProject(projectDTO, userId);

            return View();
        }
    }
}
