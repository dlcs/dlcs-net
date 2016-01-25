using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(ImageClass),
        Description = "An image. What it's all about.",
        UriTemplate = "/customers/{0}")]
    public class Image : DlcsResource
    {
    }

    public class ImageClass: Class
    {
        public override void DefineOperations()
        {
            throw new NotImplementedException();
        }
    }
}
