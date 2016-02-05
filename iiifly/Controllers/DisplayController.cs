using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using iiifly.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace iiifly.Controllers
{
    public class DisplayController : UserBaseController
    {
        public DisplayController() { }

        public DisplayController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public ActionResult ProxyManifest(string userPublicPath, string id)
        {
            var userId = GlobalData.GetUserIdFromPublicPath(userPublicPath);
            dynamic manifest = Dlcs.Dlcs.GetManifest(userPublicPath, id).Result;
            using (var db = new ApplicationDbContext())
            {
                var imageSet = db.ImageSets.Single(iset => iset.ApplicationUserId == userId && iset.Id == id);
                manifest.Label = imageSet.Label;
                manifest.Description = imageSet.Description;
            }
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            return new ContentResult
            {
                Content = manifest.ToString(),
                ContentType = "application/json"
            };
        }



        // GET: Display
        public ActionResult ImageSet(string userPublicPath, string id)
        {
            var userId = GlobalData.GetUserIdFromPublicPath(userPublicPath);
            var user = UserManager.FindById(userId);
            ImageSet imageSet = null;
            using (var db = new ApplicationDbContext())
            {
                imageSet = db.ImageSets.SingleOrDefault(iset => iset.ApplicationUserId == userId && iset.Id == id); 
            }
            if (imageSet != null)
            {
                var wrapper = new ImageSetWrapper
                {
                    ImageSet = imageSet,
                    ProxyManifest = "/display/proxymanifest/" + userPublicPath + "/" + id,
                    Images = Dlcs.Dlcs.GetImages(user.DlcsSpace, id)
                };
                return View(wrapper);
            }
            return View();
        }
    }
}