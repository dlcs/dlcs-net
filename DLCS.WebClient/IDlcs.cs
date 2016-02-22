using DLCS.WebClient.Interface;

namespace DLCS.WebClient
{
    public interface IDlcs
    {
        IQueue Queue { get; }
    }
}
