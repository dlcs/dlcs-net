using Hydra;
using Newtonsoft.Json;

namespace iiifly.Dlcs
{
    public class Space : JSONLDBase
    {
        [JsonProperty(PropertyName = "modelId")]
        public int ModelId { get; set; } // the image identifier within the space

        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "defaultTags")]
        public string[] DefaultTags { get; set; }

        [JsonProperty(Order = 14, PropertyName = "defaultMaxUnauthorised")]
        public int DefaultMaxUnauthorised { get; set; }
    }
}
