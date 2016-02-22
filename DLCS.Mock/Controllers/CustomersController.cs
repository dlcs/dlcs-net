using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLCS.Client.Model;
using Hydra.Collections;
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
        public Collection<CustomerOriginStrategy> OriginStrategies(int customerId)
        {
            var customerOriginStrategies = GetModel().CustomerOriginStrategies
                .Where(os => os.CustomerId == customerId)
                .ToArray();

            return new Collection<CustomerOriginStrategy>
            {
                IncludeContext = true,
                Members = customerOriginStrategies,
                TotalItems = customerOriginStrategies.Length,
                Id = Request.RequestUri.ToString()
            };

        }

        [HttpGet]
        public IHttpActionResult OriginStrategies(int customerId, int propertyId)
        {
            var cos = GetModel().CustomerOriginStrategies.SingleOrDefault(u => u.CustomerId == customerId && u.ModelId == propertyId);
            if (cos == null)
            {
                return NotFound();
            }
            return Ok(cos);
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
                Members = spaces,
                TotalItems = spaces.Length,
                Id = Request.RequestUri.ToString()
            };
        }

        [HttpPost]
        public HttpResponseMessage Spaces(int customerId, Space space)
        {
            if (!string.IsNullOrWhiteSpace(space.Id))
            {
                var message = "You can only POST a new Space";
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Conflict, message);
                throw new HttpResponseException(errorResponse);
            }
            if (string.IsNullOrWhiteSpace(space.Name))
            {
                var message = "The space must be given a name";
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                throw new HttpResponseException(errorResponse);
            }
            // obviously not thread safe..
            var modelId = GetModel().Spaces.Select(s => s.ModelId).Max() + 1;
            var newSpace = new Space(modelId, customerId, space.Name, DateTime.Now, space.DefaultTags, space.DefaultMaxUnauthorised);
            GetModel().Spaces.Add(newSpace);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(newSpace.Id);
            return response;
        }
        
        [HttpGet]
        public IHttpActionResult Spaces(int customerId, int propertyId)
        {
            var space = GetModel().Spaces.SingleOrDefault(s => s.CustomerId == customerId && s.ModelId == propertyId);
            if (space == null)
            {
                return NotFound();
            }
            return Ok(space);
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
