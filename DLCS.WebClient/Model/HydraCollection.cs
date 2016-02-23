using DLCS.WebClient.Model.Images;
using Newtonsoft.Json;

namespace DLCS.WebClient.Model
{
    public class HydraImageCollection : JSONLDBase
    {
        public override string Context
        {
            get {return "http://www.w3.org/ns/hydra/context.jsonld"; }
        }
        public override string Type
        {
            get { return "Collection"; }
        }

        [JsonProperty(Order = 10, PropertyName = "totalItems")]
        public int? TotalItems { get; set; }

        [JsonProperty(Order = 20, PropertyName = "member")] 
        public Image[] Members { get; set; }

        [JsonProperty(Order = 90, PropertyName = "view")]
        public PartialCollectionView View { get; set; }

    }
}
