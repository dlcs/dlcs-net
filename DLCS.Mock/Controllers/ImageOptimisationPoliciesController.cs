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
    public class ImageOptimisationPoliciesController : MockController
    {
        [HttpGet]
        public Collection<ImageOptimisationPolicy> Index()
        {
            var imageOptimisationPolicies = GetModel().ImageOptimisationPolicies.ToArray();

            return new Collection<ImageOptimisationPolicy>
            {
                IncludeContext = true,
                Members = imageOptimisationPolicies,
                TotalItems = imageOptimisationPolicies.Length,
                Id = Request.RequestUri.ToString()
            };
        }


        [HttpGet]
        public IHttpActionResult Index(string id)
        {
            var iop = GetModel().ImageOptimisationPolicies.SingleOrDefault(p => p.ModelId == id);
            if (iop != null)
            {
                return Ok(iop);
            }
            return NotFound();
        }
    }
}