using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace iiifly.Controllers
{
    public class UserBaseController : Controller
    {
        private ApplicationUserManager _userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }
    }
}
