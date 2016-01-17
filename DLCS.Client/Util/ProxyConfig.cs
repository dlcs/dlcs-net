namespace DLCS.Client.Util
{
    public class ProxyConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        //private static readonly string ProxyAddress = ConfigurationManager.AppSettings["ProxyAddress"];
        //private static readonly string ProxyPort = ConfigurationManager.AppSettings["ProxyPort"];
        //private static readonly string ProxyDomain = ConfigurationManager.AppSettings["ProxyDomain"];
        //private static readonly string ProxyUsername = ConfigurationManager.AppSettings["ProxyUsername"];
        //private static readonly string ProxyPassword = ConfigurationManager.AppSettings["ProxyPassword"];
    }
}
