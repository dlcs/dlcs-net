using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iiifly.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace iiifly.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationUserManager _userManager;

        public ImagesController()
        {
        }

        public ImagesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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


        // GET: Images
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "canCallDlcs")]
        public ActionResult Upload()
        {
            string errorMessage = null;
            string imageSet = Request.Form["imageSet"];
            if (string.IsNullOrWhiteSpace(imageSet) || imageSet.Length != 8)
            {
                return Json(new { error = "No imageSet in request" });
            }

            string filename = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if(file == null || file.ContentLength <= 0) continue;

                    //Save file content goes here
                    filename = Path.GetFileName(file.FileName) ?? file.FileName;
                    file.SaveAs(Path.Combine(GetTargetDirectory(imageSet).ToString(), filename));
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            if (errorMessage == null)
            {
                return Json(new { message = "completed: " + filename });
            }
            return Json(new { error = errorMessage, message = "error: " + filename + ", " + errorMessage });
        }

        [Authorize(Roles = "canCallDlcs")]
        [HttpPost]
        public ActionResult ExternalImage(ExternalImage image)
        {
            if (image == null)
            {
                return Json(new { error = "No external image supplied" });
            }
            if (string.IsNullOrWhiteSpace(image.ImageSet) || image.ImageSet.Length != 8)
            {
                return Json(new { error = "No imageSet in request" });
            }
            if (string.IsNullOrWhiteSpace(image.ExternalUrl))
            {
                return Json(new { error = "No external image Url in request" });
            }
            Uri uri = new Uri(image.ExternalUrl);
            string filename = uri.Segments[uri.Segments.Length - 1];
            if (string.IsNullOrWhiteSpace(filename))
            {
                return Json(new { error = "No recognisable file in request" });
            }

            var target = Path.Combine(GetTargetDirectory(image.ImageSet).ToString(), filename + ".external.json");
            System.IO.File.WriteAllText(target, JsonConvert.SerializeObject(image));

            return Json(new { message = "external: " + filename });
        }

        private DirectoryInfo GetTargetDirectory(string imageSet)
        {
            var originDirectory = new DirectoryInfo(string.Format("{0}origin\\{1}\\{2}",
                Server.MapPath(@"\"), User.Identity.GetUserId(), imageSet));
            originDirectory.Create();
            return originDirectory;
        }

        /// <summary>
        /// Take the files on disk and create a new ImageSet, creating either an immediate mode ingest or a batch in DLCS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "canCallDlcs")]
        public ActionResult CreateImageSet(string id)
        {
            var userId = User.Identity.GetUserId();
            var applicationUser = UserManager.FindById(userId);
            string space = GetSpaceForUser(applicationUser);
            var ingestImages = GetImagesFromDisk(id, space, applicationUser);
            if (ingestImages.Count == 0)
            {
                return View("noimages", id);
            }
            using (var db = new ApplicationDbContext())
            {
                db.ImageSets.Add(new ImageSet
                {
                    Id = id,
                    ApplicationUserId = userId,
                    Label = "Made by iiif.ly using DLCS",
                    Description = ""
                });
                db.SaveChanges();
            }
            if (ingestImages.Count == 1)
            {
                // process immediately, synchronously
                var result = Dlcs.Dlcs.PutImage(ingestImages[0], space);

                // inspect result... error out etc
            } 
            if (ingestImages.Count > 1)
            {
                // enqueue a batch
                var dlcsBatch = Dlcs.Dlcs.Enqueue(ingestImages);
                using (var db = new ApplicationDbContext())
                {
                    var imageSet = db.ImageSets.Find(id);
                    imageSet.DlcsBatch = dlcsBatch.Id;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ImageSet", "Display", new { user = User.GetPublicPath(), imageSet = id });
        }

        private List<IngestImage> GetImagesFromDisk(string imageSet, string space, ApplicationUser applicationUser)
        {
            int counter = 0;
            return GetTargetDirectory(imageSet).GetFiles()
                .OrderBy(fi => fi.LastWriteTime)
                .Select(fi => new IngestImage
                {
                    Id = string.Format("{0}_{1}", imageSet, fi.Name),
                    Space = GetSpaceId(space),
                    Origin = GetOrigin(fi, applicationUser, imageSet),
                    String1 = imageSet,
                    Number1 = counter++
                })
                .ToList();
        }

        private string GetOrigin(FileInfo fi, ApplicationUser applicationUser, string imageSet)
        {
            if (fi.Name.EndsWith(".external.json"))
            {
                var json = System.IO.File.ReadAllText(fi.FullName);
                var ext = JsonConvert.DeserializeObject<ExternalImage>(json);
                return ext.ExternalUrl;
            }
            var leftPart = Request.Url.GetLeftPart(UriPartial.Authority);
            return string.Format("{0}/origin/{1}/{2}/{3}", leftPart, applicationUser.Id, imageSet, fi.Name);
        }

        private int GetSpaceId(string space)
        {
            // er, this is not very RESTful...
            return int.Parse(space.Split('/').Last());
        }

        private string GetSpaceForUser(ApplicationUser applicationUser)
        {
            if (string.IsNullOrWhiteSpace(applicationUser.DlcsSpace))
            {
                // they haven't yet got a space...
                applicationUser.DlcsSpace = Dlcs.Dlcs.CreateSpace(applicationUser.Id);
                UserManager.Update(applicationUser);
            }
            return applicationUser.DlcsSpace;
        }
        
    }
}