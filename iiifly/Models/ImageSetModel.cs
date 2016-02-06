using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iiifly.Dlcs;

namespace iiifly.Models
{
    /// <summary>
    /// This is what we persist in the DB locally
    /// </summary>
    public class ImageSet
    {
        /// <summary>
        /// This is used as the String1 in the DLCS - the grouping metadata value.
        /// </summary>
        [Key]
        public string Id { get; set; }

        public string ApplicationUserId { get; set; }

        [StringLength(250)]
        public string Label { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(250)]
        public string DlcsBatch { get; set; }

        public DateTime Created { get; set; }
        public int NumberOfImages { get; set; }

    }


    /// <summary>
    /// This is enough to have a sortable list
    /// </summary>
    public class ImageSetWrapper
    {
        public ImageSet ImageSet { get; set; }
        public string ProxyManifest { get; set; }
        public List<Image> Images { get; set; }
        public string UserDisplay { get; set; }
        public string UserPublicPath { get; set; }

        public bool IsFinished()
        {
            return Images.All(im => im.Finished.HasValue);
        }
    }

    public class ImageSetList
    {
        public string UserDisplay { get; set; }
        public List<ImageSetWrapper> ImageSetWrappers { get; set; } 
    }
}
