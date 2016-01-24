using DLCS.Client.Config;
using DLCS.Client.Hydra;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    public class DlcsResource : JSONLDBase
    {
        public override string Context
        {
            get { return BaseUrl + "/contexts/" + GetType().Name + ".jsonld"; }
        }

        [JsonIgnore]
        protected string BaseUrl
        {
            get { return Constants.BaseUrl; }
        }
    }
}
