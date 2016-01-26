using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(CustomerClass), 
        Description = "The route to all of your assets in the DLCS",
        UriTemplate = "/customers/{0}")]
    public class Customer : DlcsResource
    {
        [JsonIgnore]
        public int ModelId { get; set; }

        public Customer()
        {
        }

        public Customer(int customerId, string name, string displayName)
        {
            ModelId = customerId;
            Name = name;
            DisplayName = displayName;
            Init(true, customerId);
        }
        
        [RdfProperty(Description = "The URL-friendly name of the customer", 
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "name")]
        public string Name { get; set; }

        [RdfProperty(Description = "The display name of the customer",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "displayName")]
        public string DisplayName { get; set; }


        // Hydra link properties - i.e., a link to another resource, rather than a field of the current resource.
        [HydraLink(Description = "Accounts that can log into the portal",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "portalUsers")]
        public string PortalUsers { get; set; }
        
        [HydraLink(Description = "Set of preconfigured URI patterns that will generate IIIF resources on the main DLCS site",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "namedQueries")]
        public string NamedQueries { get; set; }

        [HydraLink(Description = "Configuration for retrieving images from your endpoint(s)",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 14, PropertyName = "originStrategies")]
        public string OriginStrategies { get; set; }

        [HydraLink(Description = "Configuration for IIIF Auth Services available to you",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 15, PropertyName = "authServices")]
        public string AuthServices { get; set; }

        [HydraLink(Description = "The set of roles you have registered for the DLCS to enforce access control",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 16, PropertyName = "roles")]
        public string Roles { get; set; }

        [HydraLink(Description = "The Customer's view on the DLCS ingest queue",
            Range = "vocab:Queue", ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 17, PropertyName = "queue")]
        public string Queue { get; set; }

        [HydraLink(Description = "A space allows you to partition images, have different default rules, etc",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 18, PropertyName = "spaces")]
        public string Spaces { get; set; }
    }
    
    public class CustomerClass : Class
    {
        public CustomerClass()
        {
            BootstrapViaReflection(typeof (Customer));
        }

        public override void DefineOperations()
        {
            SupportedOperations = new[]
            {
                // No PUT or DELETE - admin functionality on customer. Can't delete yourself!
                new Operation
                {
                    Id = "_:customer_retrieve",
                    Method = "GET",
                    Label = "Obtain a customer",
                    Returns = Id
                },
                new Operation
                {
                    Id = "_:customer_update",
                    Method = "PATCH",
                    Label = "update the supplied fields of the customer",
                    Expects = Id,
                    Returns = Id
                }
            };

            // Hydra link properties - i.e., a link to another resource, rather than a field of the current resource.

            GetHydraLinkProperty("portalUsers").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations("_:customer_portalUser", "Portal User", "vocab:PortalUser");

            GetHydraLinkProperty("queue").SupportedOperations = QueueClass.GetSpecialQueueOperations();


            var spaces = GetHydraLinkProperty("spaces");
            spaces.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_space_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all of the customer's spaces",
                    Returns = Names.Hydra.Collection
                },
                new Operation
                {
                    Id = "_:customer_space_create",
                    Method = "POST",
                    Label = "Creates a new Space for this customer",
                    Description = "(doc here)",
                    Expects = "vocab:Space",
                    Returns = "vocab:Space",
                    StatusCodes = new[]
                    {
                        new Status
                        {
                            StatusCode = 201,
                            Description = "Space is ready..."
                        }
                    }
                }
            };
        }
    }
}
