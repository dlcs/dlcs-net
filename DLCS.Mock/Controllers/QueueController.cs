using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLCS.Client.Model;
using Hydra.Collections;

namespace DLCS.Mock.Controllers
{
    public class QueueController : MockController
    {
        [HttpGet]
        public Queue GetQueue(int customerId)
        {
            var queue = GetModel().Queues
                .Single(cq => cq.ModelId == customerId);

            return queue;
        }


        [HttpPost]
        public Batch PostQueue(int customerId, Collection<Image> images)
        {
            List<Image> initialisedImages = new List<Image>();

            var newBatchId = GetModel().Batches.Select(b => b.ModelId).Max() + 1;
            var batch = new Batch(newBatchId, customerId, DateTime.Now);
            GetModel().Batches.Add(batch);
            foreach (var incomingImage in images.Members)
            {
                var newImage = new Image(customerId, incomingImage.SpaceId, incomingImage.ModelId, 
                    DateTime.Now, incomingImage.Origin, incomingImage.InitialOrigin,
                    0, 0, incomingImage.MaxUnauthorised, null, null, null, true, null, 
                    incomingImage.Tags, incomingImage.String1, incomingImage.String2, incomingImage.String3,
                    incomingImage.Number1, incomingImage.Number2, incomingImage.Number3);
                initialisedImages.Add(newImage);
            }
            GetModel().Images.AddRange(initialisedImages);
            GetModel().BatchImages.Add(batch.Id, initialisedImages.Select(im => im.Id).ToList());
            return batch;
        }
    }
}
