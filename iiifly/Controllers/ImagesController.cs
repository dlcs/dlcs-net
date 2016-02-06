using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iiifly.Dlcs;
using iiifly.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace iiifly.Controllers
{
    public class ImagesController : UserBaseController
    {
        public ImagesController() { }

        public ImagesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        
        [Authorize(Roles = "canCallDlcs")]
        public ActionResult Upload()
        {
            string errorMessage = null;
            string imageSet = Request.Form["imageSet"];
            if (!GlobalData.IsValidImageSetId(imageSet))
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
                    file.SaveAs(Path.Combine(GetOriginDirectory(imageSet).ToString(), filename));
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
            if (!GlobalData.IsValidImageSetId(image.ImageSet))
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
            image.HashCode = string.Format("{0:X}", image.ExternalUrl.GetHashCode());
            string diskFilename = string.Format("{0}.{1}.external.json", filename, image.HashCode);
            var target = Path.Combine(GetOriginDirectory(image.ImageSet).ToString(), diskFilename);
            System.IO.File.WriteAllText(target, JsonConvert.SerializeObject(image));

            return Json(new { message = "external: " + filename });
        }

        private DirectoryInfo GetOriginDirectory(string imageSet)
        {
            var originDirectory = new DirectoryInfo(string.Format("{0}origin\\{1}\\{2}",
                Server.MapPath(@"\"), User.GetPublicPath(), imageSet));
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
            var space = GetSpaceForUser(applicationUser);
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
                    Created = DateTime.Now,
                    NumberOfImages = ingestImages.Count,
                    ApplicationUserId = userId,
                    Label = "Made by iiif.ly using DLCS",
                    Description = ""
                });
                db.SaveChanges();
            }
            if (ingestImages.Count == 1)
            {
                // process immediately, synchronously
                var result = Dlcs.Dlcs.PutImage(ingestImages[0]);
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
            return RedirectToAction("ImageSet", "Display", new { userPublicPath = User.GetPublicPath(), id });
        }

        private List<Image> GetImagesFromDisk(string imageSet, Space space, ApplicationUser applicationUser)
        {
            int counter = 0;
            return GetOriginDirectory(imageSet).GetFiles()
                .OrderBy(fi => fi.LastWriteTime)
                .Select(fi => GetIngestImage(fi, imageSet, space.ModelId, applicationUser, counter++))
                .ToList();
        }

        private Image GetIngestImage(FileInfo fi, string imageSet, int spaceModelId, ApplicationUser applicationUser, int index)
        {
            var img = new Image
            {
                ModelId = string.Format("{0}_{1}", imageSet, fi.Name),
                Space = spaceModelId,
                String1 = imageSet,
                Number1 = index
            };
            if (fi.Name.EndsWith(".external.json"))
            {
                var json = System.IO.File.ReadAllText(fi.FullName);
                var ext = JsonConvert.DeserializeObject<ExternalImage>(json);
                img.Origin = ext.ExternalUrl;
            }
            else
            {
                var leftPart = Request.Url.GetLeftPart(UriPartial.Authority);
                img.Origin = string.Format("{0}/origin/{1}/{2}/{3}", leftPart, 
                    GlobalData.GetPublicPath(applicationUser.Id), imageSet, fi.Name);
            }
            return img;
        }
        
        private int GetSpaceId(string space)
        {
            // er, this is not very RESTful...
            return int.Parse(space.Split('/').Last());
        }

        private Space GetSpaceForUser(ApplicationUser applicationUser)
        {
            if (string.IsNullOrWhiteSpace(applicationUser.DlcsSpace))
            {
                // they haven't yet got a space...
                var spaceTask = Dlcs.Dlcs.CreateSpace(applicationUser.Id);
                applicationUser.DlcsSpace = spaceTask; 
                UserManager.Update(applicationUser);
                var dbCtx = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                dbCtx.SaveChanges();
            }
            return new Space
            {
                Id = applicationUser.DlcsSpace,
                ModelId = GetSpaceId(applicationUser.DlcsSpace),
                Name = GlobalData.GetPublicPath(applicationUser.Id)
            };
        }
        
    }
}