using System;
using Hydra;

namespace iiifly.Dlcs
{
    public class Batch : JSONLDBase
    {
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Errors { get; set; }
        public DateTime Finished { get; set; }
    }
}
