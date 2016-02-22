using System.Web.Http;
using DLCS.HydraModel.Model;
using Hydra.Collections;

namespace DLCS.Mock.Controllers
{
    public class DlcsApiController : MockController
    {
        [HttpGet]
        public EntryPoint Index()
        {
            var ep = new EntryPoint();
            ep.Init(true);
            return ep;
        }
    }
}
