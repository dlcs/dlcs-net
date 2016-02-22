using System.Configuration;

namespace DLCS.HydraModel.Config
{
    public static class Constants
    {
        public static string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public static string Vocab = ConfigurationManager.AppSettings["Vocab"];

        public static string GetCustomerId(int internalId)
        {
            return $"{BaseUrl}/customer/{internalId}";
        }
    }
}
