using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.Client.PDFs
{
    public class MakePdfResponse : MakePdfRequest
    {
        public Error Error { get; internal set; }
    }
}
