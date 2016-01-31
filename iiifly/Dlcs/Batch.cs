using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiifly.Dlcs
{
    public class Batch
    {
        public string Id { get; set; }
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Errors { get; set; }
        public DateTime Finished { get; set; }
    }
}
