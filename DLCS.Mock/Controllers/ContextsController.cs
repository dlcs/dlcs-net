using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hydra;
using DLCS.Client.Model;
using DLCS.Mock.ApiApp;

namespace DLCS.Mock.Controllers
{
    public class ContextsController : ApiController
    {
            
        [HttpGet]
        public DlcsClassContext Index(string typeName)
        {
            return new DlcsClassContext(typeName);
        }
    }
}
