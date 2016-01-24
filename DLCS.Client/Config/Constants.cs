using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;

namespace DLCS.Client.Config
{
    public static class Constants
    {
        public static string BaseUrl = AppSettings["BaseUrl"];
        public static string Vocab = AppSettings["Vocab"];

        public static string GetCustomerId(int internalId)
        {
            return $"{BaseUrl}/customer/{internalId}";
        }
    }
}
