using DLCS.Client.Hydra.Collections;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DLCS.Mock.Controllers
{
    public class QueueController : MockController
    {
        [HttpGet]
        public Collection<JObject> Images(int customerId)
        {
            return null;
            //var queueImages = GetModel().Queues
            //    .Where(cq => cq.CustomerId == customerId)
            //    In memory model - store list of pointers to batches and images
            //    .Select(cq => cqp.GetCollectionForm()).ToArray();

            //return new Collection<JObject>
            //{
            //    IncludeContext = true,
            //    Members = portalUsers,
            //    TotalItems = portalUsers.Length,
            //    Id = Request.RequestUri.ToString()
            //};
        }
    }
}
