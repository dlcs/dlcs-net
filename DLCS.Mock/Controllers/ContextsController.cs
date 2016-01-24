using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLCS.Client.Hydra;
using DLCS.Client.Model;
using DLCS.Mock.ApiApp;

namespace DLCS.Mock.Controllers
{
    public class ContextsController : ApiController
    {
        private static Dictionary<string, object> _contexts;
            
        [HttpGet]
        public DlcsClassContext Index(string typeName)
        {
            EnsureContexts();
            return _contexts[typeName] as DlcsClassContext;
        }

        private static readonly object InitLock = new object();

        private void EnsureContexts()
        {
            if (_contexts == null)
            {
                lock (InitLock)
                {
                    if (_contexts == null)
                    {
                        _contexts = AttributeUtil.GetAttributeMap("DLCS.Client", typeof (HydraContextAttribute));
                    }
                }
            }
        }
    }
}
