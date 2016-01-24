﻿using Newtonsoft.Json;

namespace DLCS.Client.Hydra.Model
{
    public class Status : JSONLDBaseWithHydraContext
    {
        public override string Type
        {
            get { return "Status"; }
        }

        [JsonProperty(Order = 10, PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(Order = 11, PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(Order = 12, PropertyName = "description")]
        public string Description { get; set; }
    }
}
