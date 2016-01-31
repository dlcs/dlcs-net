using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iiifly.Models;
using Microsoft.AspNet.Identity.Owin;

namespace iiifly.Controllers
{
    [Authorize(Roles = "canApproveUsers")]
    public class UserAdminController : Controller
    {
        //private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public UserAdminController()
        {
        }

        public UserAdminController(ApplicationUserManager userManager) //, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            //SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

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

      //  // GET: UserAdmin/Details/5
      //  public ActionResult Details(int id)
      //  {
      //      return View();
      //  }

      //// GET: UserAdmin/Edit/5
      //  public ActionResult Edit(int id)
      //  {
      //      return View();
      //  }

      //  // POST: UserAdmin/Edit/5
      //  [HttpPost]
      //  public ActionResult Edit(int id, FormCollection collection)
      //  {
      //      try
      //      {
      //          // TODO: Add update logic here

      //          return RedirectToAction("Index");
      //      }
      //      catch
      //      {
      //          return View();
      //      }
      //  }
        
    }
}
