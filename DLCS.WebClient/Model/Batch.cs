using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.WebClient.Model
{
    public class Batch : JSONLDBase
    {
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Errors { get; set; }
        public DateTime Finished { get; set; }
    }
}
