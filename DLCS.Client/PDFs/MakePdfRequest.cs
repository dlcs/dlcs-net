using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.Client.PDFs
{
    public class MakePdfRequest
    {
        // only support 1 sequence in Phase 0
        public string ManifestUri { get; set; }

        public int Space { get; set; }                
        // if supplied and FullS3Uri is NOT supplied, will save to default pdfBucket in the space
        public string FileName { get; set; }
        public string FullS3Uri { get; set; }

    }
}
