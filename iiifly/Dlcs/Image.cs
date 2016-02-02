using System;
using Hydra;
using Newtonsoft.Json;

namespace iiifly.Dlcs
{
    /// <summary>
    /// Dlcs Image
    /// </summary>
    public class Image : JSONLDBase
    {
        [JsonProperty(PropertyName = "modelId")]
        public string ModelId { get; set; } // the image identifier within the space

        [JsonProperty(PropertyName = "infoJson")]
        public string InfoJson { get; set; }

        [JsonProperty(PropertyName = "thumbnail400")]
        public string Thumbnail400 { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "queued")]
        public DateTime? Queued { get; set; }

        [JsonProperty(PropertyName = "dequeued")]
        public DateTime? Dequeued { get; set; }

        [JsonProperty(PropertyName = "finished")]
        public DateTime? Finished { get; set; }

        [JsonProperty(PropertyName = "ingesting")]
        public bool Ingesting{ get; set; }

        [JsonProperty(PropertyName = "space")]
        public int Space { get; set; }

        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }

        [JsonProperty(PropertyName = "string1")]
        public string String1 { get; set; }

        [JsonProperty(PropertyName = "number1")]
        public int Number1 { get; set; }
    }
    

    public class ExternalImage
    {
        public string ExternalUrl { get; set; }
        public string HashCode { get; set; }
        public string ImageSet { get; set; }
    }
}
