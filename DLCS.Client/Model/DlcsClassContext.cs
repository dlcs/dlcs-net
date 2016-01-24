using DLCS.Client.Config;
using DLCS.Client.Hydra;

namespace DLCS.Client.Model
{
    public class DlcsClassContext : HydraClassContext
    {
        public string GetContextUri()
        {
            return Constants.BaseUrl + "/contexts/" + GetType().Name + ".jsonld";
        }

        public DlcsClassContext()
        {
            Add("vocab", Constants.Vocab);
        }
    }
}
