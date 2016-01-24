using Newtonsoft.Json;

namespace DLCS.Client.Hydra.Model
{
    public class Class : JSONLDBaseWithHydraContext
    {
        public override string Type
        {
            get { return "Class"; }
        }

        [JsonProperty(Order = 11, PropertyName = "subClassOf")]
        public string SubClassOf { get; set; }

        [JsonProperty(Order = 12, PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(Order = 13, PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(Order = 20, PropertyName = "supportedOperation")]
        public Operation[] SupportedOperations { get; set; }

        [JsonProperty(Order = 21, PropertyName = "supportedProperty")]
        public SupportedProperty[] SupportedProperties{ get; set; }

    }
}
