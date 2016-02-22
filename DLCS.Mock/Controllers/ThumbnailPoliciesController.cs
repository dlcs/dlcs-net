using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DLCS.HydraModel.Model;
using Hydra.Collections;

namespace DLCS.Mock.Controllers
{
    public class ThumbnailPoliciesController : MockController
    {
        [HttpGet]
        public Collection<ThumbnailPolicy> Index()
        {
            var thumbnailPolicies = GetModel().ThumbnailPolicies.ToArray();

            return new Collection<ThumbnailPolicy>
            {
                IncludeContext = true,
                Members = thumbnailPolicies,
                TotalItems = thumbnailPolicies.Length,
                Id = Request.RequestUri.ToString()
            };
        }


        [HttpGet]
        public IHttpActionResult Index(string id)
        {
            var tp = GetModel().ThumbnailPolicies.SingleOrDefault(p => p.ModelId == id);
            if (tp != null)
            {
                return Ok(tp);
            }
            return NotFound();
        }
    }
}