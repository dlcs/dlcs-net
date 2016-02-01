using Newtonsoft.Json;

namespace iiifly.Dlcs
{
    public class Space : JSONLDBase
    {
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "defaultTags")]
        public string[] DefaultTags { get; set; }

        [JsonProperty(Order = 14, PropertyName = "defaultMaxUnauthorised")]
        public int DefaultMaxUnauthorised { get; set; }
    }
}
