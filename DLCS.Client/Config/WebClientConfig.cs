namespace DLCS.Client.Config
{
    public class WebClientConfig
    {
        public bool Expect100ContinueIfProxy { get; set; }
        public ProxyConfig ProxyConfig { get; set; }
    }
}
