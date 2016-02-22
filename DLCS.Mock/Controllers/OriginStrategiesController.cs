using System.Linq;
using System.Web.Http;
using DLCS.HydraModel.Model;
using Hydra.Collections;

namespace DLCS.Mock.Controllers
{
    public class OriginStrategiesController : MockController
    {
        [HttpGet]
        public Collection<OriginStrategy> Index()
        {
            var originStrategies = GetModel().OriginStrategies.ToArray();

            return new Collection<OriginStrategy>
            {
                IncludeContext = true,
                Members = originStrategies,
                TotalItems = originStrategies.Length,
                Id = Request.RequestUri.ToString()
            };
        }


        [HttpGet]
        public IHttpActionResult Index(string id)
        {
            var originStrategy = GetModel().OriginStrategies.SingleOrDefault(os => os.ModelId == id);
            if (originStrategy != null)
            {
                return Ok(originStrategy);
            }
            return NotFound();
        }
    }
}