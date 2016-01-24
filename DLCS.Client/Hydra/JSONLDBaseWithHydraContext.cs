using Newtonsoft.Json;

namespace DLCS.Client.Hydra
{
    /// <summary>
    /// A JSON object with a Hydra context
    /// </summary>
    public class JSONLDBaseWithHydraContext : JSONLDBase
    {
        [JsonIgnore]
        protected bool IncludeContext { get; set; }

        public override string Context
        {
            get { return IncludeContext ? "http://www.w3.org/ns/hydra/context.jsonld" : null; }
        }
    }
}
