using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iiifly.Dlcs
{
    public abstract class JSONLDBase
    {
        [JsonProperty(Order = 1, PropertyName = "@context")]
        public virtual string Context { get; set; }

        [JsonProperty(Order = 2, PropertyName = "@id")]
        public string Id { get; set; }

        [JsonProperty(Order = 3, PropertyName = "@type")]
        public virtual string Type { get; set; }
    }
}
