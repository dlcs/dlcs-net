using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using iiifly.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace iiifly.Controllers
{
    [Authorize(Roles = "canApproveUsers")]
    public class UserAdminController : UserBaseController
    {
        public UserAdminController(){ }

        public UserAdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: UserAdmin; TODO: paging
        public ActionResult Index()
        {
            // TODO: http://devproconnections.com/aspnet-mvc/aspnet-mvc-paging-done-perfectly
            const string sql = @"
                SELECT AspNetUsers.Id AS UserId, AspNetUsers.UserName, AspNetUsers.Email,
                AspNetUsers.DisplayName, AspNetUsers.Affiliation, 
                CAST(MAX(CASE AspNetRoles.Name WHEN 'canApproveUsers' THEN 1 ELSE 0 END) AS BIT) CanApproveUsers,
                CAST(MAX(CASE AspNetRoles.Name WHEN 'canCallDlcs'	   THEN 1 ELSE 0 END) AS BIT) CanCallDlcs
                FROM AspNetUsers 
                LEFT JOIN AspNetUserRoles ON  AspNetUserRoles.UserId = AspNetUsers.Id 
                LEFT JOIN AspNetRoles ON AspNetRoles.Id = AspNetUserRoles.RoleId
                GROUP BY AspNetUsers.Id, AspNetUsers.UserName, AspNetUsers.Email, 
                AspNetUsers.DisplayName, AspNetUsers.Affiliation
                ORDER BY UserName";

            List<UserRoleModel> users;
            using (var ctx = new ApplicationDbContext())
            {
                users = ctx.Database.SqlQuery<UserRoleModel>(sql).ToList();
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult SetRoleState(ToggleRoleState state)
        {
            if (state.InRole)
            {
                UserManager.AddToRole(state.UserId, state.RoleName);
            }
            else
            {
                UserManager.RemoveFromRole(state.UserId, state.RoleName);
            }
            Response.ContentType = "application/json";
            return Json(state);
        }
    }
}
