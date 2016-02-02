using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLCS.Client.Config;
using Hydra;
using DLCS.Mock.ApiApp;
using Hydra.Model;

namespace DLCS.Mock.Controllers
{
    public class DocumentationController : ApiController
    {
        private static Dictionary<string, object> _supportedClasses;

        [HttpGet]
        public ApiDocumentation Vocab()
        {
            EnsureClasses();
            var classes = _supportedClasses.Values.Cast<Class>().ToArray();
            var vocab = new ApiDocumentation(Constants.Vocab, Constants.Vocab, classes);
            return vocab;
        }

        private static readonly object InitLock = new object();

        private void EnsureClasses()
        {
            if (_supportedClasses == null)
            {
                lock (InitLock)
                {
                    if (_supportedClasses == null)
                    {
                        _supportedClasses = AttributeUtil.GetAttributeMap("DLCS.Client", typeof(HydraClassAttribute));
                    }
                }
            }
        }
    }
}
