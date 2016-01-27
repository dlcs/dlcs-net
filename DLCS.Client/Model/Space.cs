using System;
using System.Collections.Generic;
using System.Linq;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(SpaceClass),
           Description = "You can use a Space to partition your images and give them different default settings.",
           UriTemplate = "/customers/{0}/spaces/{1}")]
    public class Space : DlcsResource
    {
        // Mock Model fields
        [JsonIgnore]
        public int ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }



        public Space() { }

        public Space(int modelId, int customerId, string name, DateTime created, string[] defaultTags, int defaultMaxUnauthorised)
        {

            ModelId = modelId;
            CustomerId = customerId;
            Name = name;
            Created = created;
            DefaultTags = defaultTags;
            DefaultMaxUnauthorised = defaultMaxUnauthorised;
            Init(true, customerId, ModelId);
        }

        [RdfProperty(Description = "Space name",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "name")]
        public string Name { get; set; }

        [RdfProperty(Description = "Date the space was created",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "created")]
        public DateTime Created { get; set; }

        [RdfProperty(Description = "Default tags to apply to images created in this space",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "defaultTags")]
        public string[] DefaultTags { get; set; }

        [RdfProperty(Description = "Default size at which role-based authorisation will be enforced. -1=open, 0=always require auth",
            Range = Names.XmlSchema.Integer, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 14, PropertyName = "defaultMaxUnauthorised")]
        public int DefaultMaxUnauthorised { get; set; }

        [HydraLink(Description = "Default roles that will be applied to images in this space",
            Range = Names.Hydra.Collection, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "defaultRoles")]
        public string DefaultRoles { get; set; }

        [HydraLink(Description = "All the images in the space",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "images")]
        public string Images { get; set; }
    }

    public class SpaceClass : Class
    {
        public SpaceClass()
        {
            BootstrapViaReflection(typeof(Space));
        }

        public override void DefineOperations()
        {
            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                "_:customer_space_", "Space", Id,
                "GET", "PUT", "PATCH", "DELETE");

            var images = GetHydraLinkProperty("images");
            images.SupportedOperations = CommonOperations
                .GetStandardCollectionOperations("_:customer_space_image_", "Image", "vocab:Image");
            images.SupportedOperations.WithMethod("POST").Description =
                "Push an image for immediate processing, asynchronously. Might fail or timeout. This operation is rate-limited.";

            GetHydraLinkProperty("defaultRoles").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations("_:customer_space_defaultRole_", "Role", "vocab:Role");
        }
    }
}
