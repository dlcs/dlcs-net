using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iiifly.Models
{
    /// <summary>
    /// For rendering on the client
    /// </summary>
    public class DlcsImage
    {
        public string Id { get; set; }
        public string InfoJson { get; set; }
        public string Thumbnail400 { get; set; }
        public int Number1 { get; set; }
    }

    /// <summary>
    /// For posting, putting or patching
    /// </summary>
    public class IngestImage
    {
        public string Id { get; set; }
        public int Space { get; set; }
        public string Origin { get; set; }
        public string String1 { get; set; }
        public int Number1 { get; set; }
    }

    public class ExternalImage
    {
        public string ExternalUrl { get; set; }
        public string ImageSet { get; set; }
    }
}
