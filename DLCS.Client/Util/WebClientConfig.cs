using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.Client.Util
{
    public class WebClientConfig
    {
        public bool Expect100ContinueIfProxy { get; set; }
        public ProxyConfig ProxyConfig { get; set; }
    }
}
