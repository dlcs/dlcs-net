using System.Web.Http;
using DLCS.Client.Model;

namespace DLCS.Mock.Controllers
{
    public class DlcsApiController : ApiController
    {
        [HttpGet]
        public EntryPoint EntryPoint()
        {
            var ep = new EntryPoint();
            ep.Init(true);
            return ep;
        }
    }
}
