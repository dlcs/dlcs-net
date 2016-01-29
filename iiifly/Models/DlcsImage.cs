using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiifly.Models
{
    public class DlcsImage
    {
        public Guid UserId { get; set; }
        public string ImageId { get; set; }
        public string Thumbnail400 { get; set; }

        // FOr use in a 
        public string Label { get; set; }

        public string Description { get; set; }
    }
}
