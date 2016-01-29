using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiifly.Models
{
    public class SpaceMapping
    {
        public const int CustomerId = 9;
        /// <summary>
        /// This is the user ID from the Membership database
        /// </summary>
        public Guid SpaceMappingId { get; set; }
        public string DlcsSpace { get; set; }
        public int SpaceId { get; set; }
    }
}
