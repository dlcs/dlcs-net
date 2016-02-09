using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using HydraImageCollection = Hydra.Collections.Collection<iiifly.Dlcs.Image>;
using iiifly.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace iiifly.Dlcs
{
    public class Dlcs
    {
        private static HttpClient GetClient()
        {
            return HttpClientBuilder.GetHttpClient();
        }

        private static T ReadViaStringAsync<T>(HttpContent content)
        {
            var s = content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(s);
        }

        private static readonly string CustomerUrl = 
            ConfigurationManager.AppSettings["DlcsEntryPoint"] + "/customers/" +
            ConfigurationManager.AppSettings["dlcs-customer-id"];

        public static string CreateSpace(string userId)
        {
            var space = new Space
            {
                Name = GlobalData.GetPublicPath(userId)
            };
            var spacesUri = CustomerUrl + "/spaces";
            var client = GetClient();
            HttpResponseMessage response = client.PostAsJsonAsync(spacesUri, space).Result;
            if (response.IsSuccessStatusCode)
            {
                // Get the URI of the created resource.
                Uri newSpace = response.Headers.Location;
                return newSpace.ToString();
            }

            return null;
        }

        public static Batch Enqueue(List<Image> ingestImages)
        {
            var images = new HydraImageCollection
            {
                Members = ingestImages.ToArray()
            };
            var queueUri = CustomerUrl + "/queue";
            HttpResponseMessage response = GetClient().PostAsJsonAsync(queueUri, images).Result;
            if (response.IsSuccessStatusCode)
            {
                // Get the URI of the created resource
                return ReadViaStringAsync<Batch>(response.Content);
                //return response.Content.ReadAsAsync<Batch>().Result;
            }
            return null;
        }

        /// <summary>
        /// Immediate mode
        /// </summary>
        /// <param name="ingestImage"></param>
        /// <returns></returns>
        public static Image PutImage(Image ingestImage)
        {
            var putUri = CustomerUrl + "/spaces/" + ingestImage.Space + "/images/" + ingestImage.ModelId;
            var client = GetClient();
            HttpResponseMessage response = client.PutAsJsonAsync(putUri, ingestImage).Result;
            if (response.IsSuccessStatusCode)
            {
                return ReadViaStringAsync<Image>(response.Content);
                //return response.Content.ReadAsAsync<Image>().Result;
            }
            return null;
        }

        public static List<Image> GetImages(string space, string string1)
        {
            string requestUrl = space + "/images?string1=" + string1;
            var client = GetClient();
            HttpResponseMessage response = client.GetAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {

                HydraImageCollection hc = ReadViaStringAsync<HydraImageCollection>(response.Content);
                // HydraImageCollection hc = response.Content.ReadAsAsync<HydraImageCollection>().Result;
                return hc.Members.OrderBy(img => img.Number1).ToList();
            }

            return new List<Image>();
        }

        private static string _manifestTemplate = ConfigurationManager.AppSettings["dlcs-manifest-template"];
        private static int _customerId = int.Parse(ConfigurationManager.AppSettings["dlcs-customer-id"]);

        public static dynamic GetManifest(string user, string id)
        {
            var dlcsManifestUri = string.Format(_manifestTemplate, _customerId, user, id);
            var client = GetClient();
            HttpResponseMessage response = client.GetAsync(dlcsManifestUri).Result;
            var body = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(body);
        }
    }
}
