using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLCS.Client.Config;
using DLCS.Client.Model;

namespace DLCS.Mock.Controllers
{
    public class DlcsApiController : ApiController
    {
        [HttpGet]
        public EntryPoint EntryPoint()
        {
            return new EntryPoint
            {
                Id = Constants.BaseUrl + "/",
                Customers = Constants.BaseUrl + "/customers"
            };
        }
    }
}
