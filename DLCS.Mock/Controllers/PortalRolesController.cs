using System.Linq;
using System.Web.Http;
using DLCS.Client.Model;
using Hydra.Collections;

namespace DLCS.Mock.Controllers
{
    public class PortalRolesController : MockController
    {
        [HttpGet]
        public Collection<PortalRole> Index()
        {
            var portalRoles = GetModel().PortalRoles.ToArray();

            return new Collection<PortalRole>
            {
                IncludeContext = true,
                Members = portalRoles,
                TotalItems = portalRoles.Length,
                Id = Request.RequestUri.ToString()
            };
        }

        [HttpGet]
        public IHttpActionResult Index(string id)
        {
            var portalRole = GetModel().PortalRoles.SingleOrDefault(pr => pr.ModelId == id);
            if (portalRole != null)
            {
                return Ok(portalRole);
            }
            return NotFound();
        }

        [HttpGet]
        public Collection<PortalRole> RolesForUser(int customerId, string portalUserId)
        {
            var userid = Request.RequestUri.AbsoluteUri.Replace("/roles", "");
            // need to make this use last part.,,
            var roleIdsForUser = GetModel().PortalUserRoles[userid];
            var roles = GetModel().PortalRoles.Where(pr => roleIdsForUser.Contains(pr.Id)).ToArray();

            return new Collection<PortalRole>
            {
                IncludeContext = true,
                Members = roles,
                TotalItems = roles.Length,
                Id = Request.RequestUri.ToString()
            };
        }
    }
}
