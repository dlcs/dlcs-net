using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.Client.Images
{
    public class ImageQuery
    {
        public int? Space { get; set; }
     

        // these will become StringReference1, 2, 3
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Reference3 { get; set; }

        public long? NumberReference1 { get; set; }
        public long? NumberReference2 { get; set; }
        public long? NumberReference3 { get; set; }
        
        public string[] Tags { get; set; }
        public string[] Roles { get; set; }
    }
}
