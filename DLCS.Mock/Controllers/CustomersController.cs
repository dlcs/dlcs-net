using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DLCS.Client.Hydra.Collections;
using DLCS.Client.Model;
using Newtonsoft.Json.Linq;

namespace DLCS.Mock.Controllers
{
    public class CustomersController : MockController
    {
        [HttpGet]
        public Collection<JObject> Index()
        {
            var customers = GetModel().Customers
                .Select(c => c.GetCollectionForm()).ToArray();

            return new Collection<JObject>
            {
                IncludeContext = true,
                Members = customers,
                TotalItems = customers.Length,
                Id = Request.RequestUri.ToString()
            };
        }


        [HttpGet]
        public IHttpActionResult Index(int customerId)
        {
            var customer = GetModel().Customers.SingleOrDefault(c => c.ModelId == customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public Collection<JObject> PortalUsers(int customerId)
        {
            var portalUsers = GetModel().PortalUsers
                .Where(p => p.CustomerId == customerId)
                .Select(p => p.GetCollectionForm()).ToArray();

            return new Collection<JObject>
            {
                IncludeContext = true,
                Members = portalUsers,
                TotalItems = portalUsers.Length,
                Id = Request.RequestUri.ToString()
            };

        }


        [HttpGet]
        public Collection<JObject> Spaces(int customerId)
        {
            var spaces = GetModel().Spaces
                .Where(p => p.CustomerId == customerId)
                .Select(p => p.GetCollectionForm()).ToArray();

            return new Collection<JObject>
            {
                IncludeContext = true,
                Members = portalUsers,
                TotalItems = portalUsers.Length,
                Id = Request.RequestUri.ToString()
            };

        }

        [HttpGet]
        public IHttpActionResult PortalUsers(int customerId, string propertyId)
        {
            var user = GetModel().PortalUsers.SingleOrDefault(u => u.CustomerId == customerId && u.ModelId == propertyId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        
        [HttpGet]
        public IHttpActionResult PortalUsers(int customerId, string propertyId)
        {
            var user = GetModel().PortalUsers.SingleOrDefault(u => u.CustomerId == customerId && u.ModelId == propertyId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public IHttpActionResult Queue(int customerId)
        {
            var queue = GetModel().Queues.SingleOrDefault(q => q.ModelId == customerId);
            if (queue == null)
            {
                return NotFound();
            }
            return Ok(queue);
        }
    }
}
