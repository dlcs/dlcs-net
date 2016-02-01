using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using iiifly.Models;

namespace iiifly.Dlcs
{
    public class Dlcs
    {
        private static readonly string CustomerUrl = 
            ConfigurationManager.AppSettings["DlcsEntryPoint"] + 
            ConfigurationManager.AppSettings["dlcs-customer-uri"];

        public static async Task<string> CreateSpace(string userId)
        {
            using (var client = new HttpClient())
            {
                var space = new Space {Name = GlobalData.GetPublicPath(userId)};
                var response = await client.PostAsJsonAsync(CustomerUrl + "/spaces", space);
                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.
                    Uri newSpace = response.Headers.Location;
                    return newSpace.ToString();
                }
            }
            return null;
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

        public static string GetImageSets
    }
}
