using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using iiifly.Models;

namespace iiifly.Dlcs
{
    public class Dlcs
    {

        public static string CreateSpace(string userId)
        {
            throw new NotImplementedException();
        }

        public static Batch Enqueue(List<IngestImage> ingestImages)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Immediate mode
        /// </summary>
        /// <param name="ingestImage"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public static string PutImage(IngestImage ingestImage, string space)
        {
            throw new NotImplementedException();
        }
    }
}
