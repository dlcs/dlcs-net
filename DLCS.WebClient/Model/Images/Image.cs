﻿using System;
using Newtonsoft.Json;

namespace DLCS.WebClient.Model.Images
{
    public class Image : JSONLDBase
    {
        [JsonProperty(Order = 10, PropertyName = "id")]
        public string ModelId { get; set; }

        [JsonProperty(Order = 10, PropertyName = "space")]
        public int? Space { get; set; }

        [JsonProperty(Order = 11, PropertyName = "infoJson")]
        public string InfoJson { get; set; }

        [JsonProperty(Order = 12, PropertyName = "degradedInfoJson")]
        public string DegradedInfoJson { get; set; }

        [JsonProperty(Order = 13, PropertyName = "thumbnailInfoJson")]
        public string ThumbnailInfoJson { get; set; }

        [JsonProperty(Order = 13, PropertyName = "thumbnail400")]
        public string Thumbnail400 { get; set; }

        [JsonProperty(Order = 14, PropertyName = "created")]
        public DateTime? Created { get; set; }

        [JsonProperty(Order = 15, PropertyName = "origin")]
        public string Origin { get; set; }

        [JsonProperty(Order = 16, PropertyName = "initialOrigin")]
        public string InitialOrigin { get; set; }

        [JsonProperty(Order = 17, PropertyName = "maxUnauthorised")]
        public int? MaxUnauthorised { get; set; }

        [JsonProperty(Order = 20, PropertyName = "width")]
        public int? Width { get; set; }

        [JsonProperty(Order = 20, PropertyName = "height")]
        public int? Height { get; set; }

        [JsonProperty(Order = 30, PropertyName = "queued")]
        public DateTime? Queued { get; set; }

        [JsonProperty(Order = 31, PropertyName = "dequeued")]
        public DateTime? Dequeued { get; set; }

        [JsonProperty(Order = 32, PropertyName = "finished")]
        public DateTime? Finished { get; set; }

        [JsonProperty(Order = 33, PropertyName = "ingesting")]
        public bool Ingesting { get; set; }

        [JsonProperty(Order = 40, PropertyName = "error")]
        public string Error { get; set; }

        // metadata

        [JsonProperty(Order = 40, PropertyName = "tags")]
        public string[] Tags { get; set; }

        [JsonProperty(Order = 41, PropertyName = "string1")]
        public string String1 { get; set; }

        [JsonProperty(Order = 42, PropertyName = "string2")]
        public string String2 { get; set; }

        [JsonProperty(Order = 43, PropertyName = "string3")]
        public string String3 { get; set; }

        [JsonProperty(Order = 51, PropertyName = "number1")]
        public long Number1 { get; set; }

        [JsonProperty(Order = 52, PropertyName = "number2")]
        public long Number2 { get; set; }

        [JsonProperty(Order = 53, PropertyName = "number3")]
        public long Number3 { get; set; }


        // Hydra Link properties

        [JsonProperty(Order = 70, PropertyName = "roles")]
        public string[] Roles { get; set; }

        [JsonProperty(Order = 71, PropertyName = "batch")]
        public string Batch { get; set; }

        [JsonProperty(Order = 80, PropertyName = "imageOptimisationPolicy")]
        public string ImageOptimisationPolicy { get; set; }

        [JsonProperty(Order = 81, PropertyName = "thumbnailPolicy")]
        public string ThumbnailPolicy { get; set; }

    }
}
