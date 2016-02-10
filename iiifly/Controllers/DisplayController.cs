using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
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


        [AllowAnonymous]
        public ActionResult ProxyManifest(string userPublicPath, string id)
        {
            var userId = GlobalData.GetUserIdFromPublicPath(userPublicPath);
            dynamic manifest = Dlcs.Dlcs.GetManifest(userPublicPath, id);
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

        [AllowAnonymous]
        public ActionResult ImageSet(string userPublicPath, string id)
        {
            if (string.IsNullOrWhiteSpace(userPublicPath))
            {
                return ShowRecentImageSets();
            }
            var userId = GlobalData.GetUserIdFromPublicPath(userPublicPath);
            var imageSetCreator = UserManager.FindById(userId);
            if (string.IsNullOrWhiteSpace(id))
            {
                return ShowUserImageSets(imageSetCreator);
            }
            ImageSet imageSet;
            using (var db = new ApplicationDbContext())
            {
                imageSet = db.ImageSets.SingleOrDefault(iset => iset.ApplicationUserId == userId && iset.Id == id); 
            }
            if (imageSet != null)
            {
                var wrapper = GetImageSetWrapper(imageSetCreator, imageSet, true);
                return View(wrapper);
            }
            return View();
        }

        private ImageSetWrapper GetImageSetWrapper(ImageSet imageSet, bool loadImages = false)
        {
            var user = UserManager.FindById(imageSet.ApplicationUserId);
            return GetImageSetWrapper(user, imageSet, loadImages);
        }

        private ImageSetWrapper GetImageSetWrapper(ApplicationUser user, ImageSet imageSet, bool loadImages)
        {
            var userPublicPath = GlobalData.GetPublicPath(user.Id);
            var isw = new ImageSetWrapper
            {
                UserPublicPath = userPublicPath,
                UserDisplay = GetDisplayForm(user),
                ImageSet = imageSet,
                ProxyManifest = "/display/proxymanifest/" + userPublicPath + "/" + imageSet.Id
            };
            if (loadImages)
            {
                isw.Images = Dlcs.Dlcs.GetImages(user.DlcsSpace, imageSet.Id);
                foreach (var image in isw.Images)
                {
                    if (image.Finished.HasValue && image.Finished.Value.Year < 2000)
                    {
                        image.Finished = null;
                    }
                }
            }
            return isw;
        }

        private string GetDisplayForm(ApplicationUser user)
        {
            var s = user.UserName;
            if (!string.IsNullOrWhiteSpace(user.DisplayName))
            {
                s = user.DisplayName;
            }
            if (!string.IsNullOrWhiteSpace(user.Affiliation))
            {
                s += " (" + user.Affiliation + ")";
            }
            return s;
        }

        private ActionResult ShowRecentImageSets()
        {
            using (var db = new ApplicationDbContext())
            {
                var imageSets = db.ImageSets.OrderByDescending(iset => iset.Created).Take(20).ToList();
                var setList = new ImageSetList
                {
                    ImageSetWrappers = imageSets.Select(iset => GetImageSetWrapper(iset)).ToList()
                };
                return View("ImageSetList", setList);
            }
        }

        private ActionResult ShowUserImageSets(ApplicationUser user)
        {
            var setList = new ImageSetList {UserDisplay = GetDisplayForm(user)};

            using (var db = new ApplicationDbContext())
            {
                var imageSets = db.ImageSets.Where(iset => iset.ApplicationUserId == user.Id).ToList();
                setList.ImageSetWrappers = imageSets.Select(iset => GetImageSetWrapper(user, iset, false)).ToList();
            }
            return View("ImageSetList", setList);
        }


        [Authorize(Roles = "canCallDlcs")]
        public ActionResult UpdateImageSet()
        {
            var currentUser = User.Identity.GetUserId();
            var imageSetId = Request.Form["ImageSet.Id"];
            var userPublicPath = Request.Form["UserPublicPath"];
            var label = Request.Form["ImageSet.Label"];
            var description = Request.Form["ImageSet.Description"];

            using (var db = new ApplicationDbContext())
            {
                var imageSet = db.ImageSets.SingleOrDefault(iset => iset.Id == imageSetId);
                if (imageSet != null && (imageSet.ApplicationUserId == currentUser || User.IsInRole("canApproveUsers")))
                {
                    if (!string.IsNullOrWhiteSpace(label))
                        imageSet.Label = label;
                    if (!string.IsNullOrWhiteSpace(description))
                        imageSet.Description = description;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("ImageSet", new { userPublicPath, id = imageSetId});
        }
    }
}