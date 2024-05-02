using DevTeamUp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Security.Claims;

namespace DevTeamUp.Filters
{
    public class ProfileCompletionFilter: ActionFilterAttribute
    {
        private readonly  UserService _userService;
        public ProfileCompletionFilter(UserService userService)
        {
            _userService = userService;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var path = filterContext.HttpContext.Request.Path;
            

            if(path == "/Profile/ProfileInit"  )
            {
                return;
            }

            var user = filterContext.HttpContext.User;
            var isUserAuthenticated = user.Identity.IsAuthenticated;

            // Проверяем, аутентифицирован ли пользователь
            if (!isUserAuthenticated)
            {
                // Если пользователь не аутентифицирован, перенаправляем его на страницу входа
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                if (!_userService.IsProfileCompleted(int.Parse(userId)))
                {

                    //filterContext.Result = new RedirectResult("~/Profile/ProfileInit");
                    filterContext.Result = new RedirectToActionResult("ProfileInit", "Profile", null);
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }


    }
}
