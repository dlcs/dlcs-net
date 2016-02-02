using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using DLCS.Client.Model;
using Hydra.Collections;

namespace DLCS.Mock.Controllers
{
    public class SpaceImagesController : MockController
    {
        [HttpGet]
        public Image GetImage(int customerId, int spaceId, string id)
        {
            var image = GetModel().Images.SingleOrDefault(
                im => im.CustomerId == customerId 
                && im.SpaceId == spaceId
                && im.ModelId == id);
            return image;
        }

        [HttpGet]
        // GET: SpaceImages
        public Collection<Image> Image(int customerId, int spaceId, string string1 = null)
        {
            var images = GetModel().Images.Where(im => im.CustomerId == customerId && im.SpaceId == spaceId);
            if (string1 != null)
            {
                images = images.Where(im => im.String1 == string1);
            }
            var hc = new Collection<Image>();
            hc.Members = images.ToArray();
            hc.TotalItems = hc.Members.Length;
            hc.Id = Request.RequestUri.ToString();
            return hc;
        }

        [HttpPut]
        public Image PutImage(int customerId, int spaceId, int id, Image incomingImage)
        {
            var newImage = new Image(customerId, spaceId,  incomingImage.ModelId,
                    DateTime.Now, incomingImage.Origin, incomingImage.InitialOrigin,
                    0, 0, incomingImage.MaxUnauthorised, null, null, null, true, null,
                    incomingImage.Tags, incomingImage.String1, incomingImage.String2, incomingImage.String3,
                    incomingImage.Number1, incomingImage.Number2, incomingImage.Number3);
            GetModel().Images.Add(newImage);
            return newImage;
        }
    }
}