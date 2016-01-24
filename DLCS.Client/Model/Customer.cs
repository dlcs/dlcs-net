using DLCS.Client.Config;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        // hydra links:
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
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
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
                new Operation
                {
                    Id = "_:customer_retrieve",
                    Method = "GET",
                    Label = "Obtain a customer",
                    Returns = Id
                }
            };

            var portalUsers = GetHydraLinkProperty("portalUsers");
            portalUsers.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_portalUser_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all portal users",
                    Returns = Names.Hydra.Collection
                },
                new Operation
                {
                    Id = "_:customer_portalUser_create",
                    Method = "POST",
                    Label = "Creates a new Portal user for this customer",
                    Description = "(doc here)",
                    Expects = "vocab:PortalUser",
                    Returns = "vocab:PortalUser",
                    StatusCodes = new[]
                    {
                        new Status
                        {
                            StatusCode = 201,
                            Description = "User is ready..."
                        }
                    }
                }
            };


            var queue = GetHydraLinkProperty("queue");
            queue.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves a view of the top of the queue",
                    Returns = Names.Hydra.Collection
                },
                new Operation
                {
                    Id = "_:customer_queue_create_batch",
                    Method = "POST",
                    Label = "Submit an array of Image and get a batch back",
                    Description = "(doc here)",
                    // TODO: I want to say Expects: vocab:Image[] - but how do we do that?
                    // We've lost information here - how does a client know how to send a collection of images? How do we declare that?
                    // When we say that something returns a Collection that's OK, because the client can inspect the members of the collection.
                    // but how do we declare that the API user should POST a collection?
                    Expects = Names.Hydra.Collection, // 
                    // maybe it's something else - like:
                    // Expects = "vocab:ImageList"
                    // where we define another Class in the documentation, and ImageList just has an Images property []
                    // but that is no different from the members of a collection.
                    // see http://lists.w3.org/Archives/Public/public-hydra/2016Jan/0087.html
                    // From that I'll leave as Collection and rely on out-of-band knowledge until Hydra catches up.
                    Returns = "vocab:Batch",
                    StatusCodes = new[]
                    {
                        new Status
                        {
                            StatusCode = 202,
                            Description = "Job has been accepted"
                        }
                    }
                }
            };

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
