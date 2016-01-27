using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(ImageClass),
        Description = "An image. What it's all about.",
        UriTemplate = "/customers/{0}/spaces/{1}/images/{2}")]
    public class Image : DlcsResource
    {
        [JsonIgnore]
        public string ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public int SpaceId { get; set; }

        public Image()
        {
        }

        public Image(int customerId, int spaceId, string imageId, string name, string displayName)
        {
            ModelId = imageId;
            CustomerId = customerId;
            SpaceId = spaceId;
            Init(true, customerId, spaceId, ModelId);
        }


        [RdfProperty(Description = "info.json URI",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "infoJson")]
        public string InfoJson { get; set; }

        [RdfProperty(Description = "Degraded info.json URI",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "degradedInfoJson")]
        public string DegradedInfoJson { get; set; }

        [RdfProperty(Description = "Thumbnail info.json URI",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "thumbnailInfoJson")]
        public string ThumbnailInfoJson { get; set; }

        [RdfProperty(Description = "Date the image was added",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 14, PropertyName = "created")]
        public DateTime Created { get; set; }

        [RdfProperty(Description = "Origin endpoint from where the original image can be acquired",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 15, PropertyName = "origin")]
        public string Origin { get; set; }

        [RdfProperty(Description = "Endpoint to use the first time the image is retrieved",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 16, PropertyName = "initialOrigin")]
        public string InitialOrigin { get; set; }

        [RdfProperty(Description = "Maximum size of request allowed before roles are enforced " +
                                   "- relates to the effective WHOLE image size, not the individual tile size." +
                                   " 0 = No open option, -1 (default) = no authorisation",
            Range = Names.XmlSchema.Integer, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 17, PropertyName = "maxUnauthorised")]
        public int MaxUnauthorised { get; set; }


        [RdfProperty(Description = "Tile source width",
            Range = Names.XmlSchema.Integer, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "width")]
        public int Width { get; set; }
        
        [RdfProperty(Description = "Tile source height",
            Range = Names.XmlSchema.Integer, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "height")]
        public int Height { get; set; }
        
        [RdfProperty(Description = "When the image was added to the queue",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 30, PropertyName = "queued")]
        public DateTime Queued { get; set; }

        [RdfProperty(Description = "When the image was taken off the queue",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 31, PropertyName = "dequeued")]
        public DateTime Dequeued { get; set; }

        [RdfProperty(Description = "When the image processing finished (image ready)",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 32, PropertyName = "finished")]
        public DateTime Finished { get; set; }

        [RdfProperty(Description = "Is the image currently being ingested?",
            Range = Names.XmlSchema.Boolean, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 33, PropertyName = "ingesting")]
        public DateTime Ingesting { get; set; }

        [RdfProperty(Description = "Reported errors with this image",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 40, PropertyName = "error")]
        public string Error { get; set; }

        // metadata

        [RdfProperty(Description = "Image tags",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 40, PropertyName = "tags")]
        public string[] Tags { get; set; }

        [RdfProperty(Description = "String reference 1",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 41, PropertyName = "string1")]
        public string String1 { get; set; }

        [RdfProperty(Description = "String reference 2",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 42, PropertyName = "string2")]
        public string String2 { get; set; }

        [RdfProperty(Description = "String reference 3",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 43, PropertyName = "string3")]
        public string String3 { get; set; }

        [RdfProperty(Description = "Number reference 1",
            Range = Names.XmlSchema.NonNegativeInteger, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 51, PropertyName = "number1")]
        public long Number1 { get; set; }

        [RdfProperty(Description = "Number reference 2",
            Range = Names.XmlSchema.NonNegativeInteger, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 52, PropertyName = "number2")]
        public long Number2 { get; set; }

        [RdfProperty(Description = "Number reference 3",
            Range = Names.XmlSchema.NonNegativeInteger, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 53, PropertyName = "number3")]
        public long Number3 { get; set; }


        // Hydra Link properties
        
        [HydraLink(Description = "The role or roles that a user must possess to view this image above maxUnauthorised",
            Range = Names.Hydra.Collection, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 70, PropertyName = "roles")]
        public string Roles { get; set; }

        [HydraLink(Description = "The batch this image was ingested in (most recently). Might be blank if the batch has been archived or the image as ingested in immediate mode.",
            Range = "vocab:Batch", ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 71, PropertyName = "batch")]
        public string Batch { get; set; }

    }

    public class ImageClass: Class
    {
        string operationId = "_:customer_space_image_";
        public ImageClass()
        {
            BootstrapViaReflection(typeof(Image));
        }
        public override void DefineOperations()
        {
            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                operationId, "Image", Id,
                "GET", "PUT", "PATCH", "DELETE"); // TODO - what is and is not allwoed here...?


            GetHydraLinkProperty("roles").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "_role_", "Role", "vocab:Role");

            GetHydraLinkProperty("batch").SupportedOperations = CommonOperations
                .GetStandardResourceOperations(operationId + "_batch", "Batch", Id,
                "GET", "PUT", "PATCH", "DELETE");
        }
    }
}
