using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(CustomerClass))]
    [HydraContext(typeof(CustomerContext))]
    public class Customer : DlcsResource
    {
        // Id must be assigned by server

        public override string Type
        {
            get { return "Customer"; }
        }

        // rdf:Property properties

        [JsonProperty(Order = 11, PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(Order = 12, PropertyName = "displayName")]
        public string DisplayName { get; set; }


        // hydra links:

        [JsonProperty(Order = 12, PropertyName = "portalUsers")]
        public string PortalUsers { get; set; }

        [JsonProperty(Order = 13, PropertyName = "namedQueries")]
        public string NamedQueries { get; set; }
        
        [JsonProperty(Order = 14, PropertyName = "originStrategies")]
        public string OriginStrategies { get; set; }
        
        [JsonProperty(Order = 15, PropertyName = "authServices")]
        public string AuthServices { get; set; }
        
        [JsonProperty(Order = 16, PropertyName = "roles")]
        public string Roles { get; set; }

        [JsonProperty(Order = 17, PropertyName = "queue")]
        public string Queue { get; set; }

        [JsonProperty(Order = 18, PropertyName = "spaces")]
        public string Spaces { get; set; }
    }

    public class CustomerContext : DlcsClassContext
    {
        public CustomerContext()
        {
            Add("Customer", "vocab:Customer");
            Add("name", "vocab:Customer/name");
            Add("displayName", "vocab:Customer/displayName");
            Add("portalUsers", new Link { Id = "vocab:Customer/portalUsers" });
            Add("namedQueries", new Link { Id = "vocab:Customer/namedQueries" });
            Add("originStrategies", new Link { Id = "vocab:Customer/originStrategies" });
            Add("authServices", new Link { Id = "vocab:Customer/authServices" });
            Add("roles", new Link { Id = "vocab:Customer/roles" });
            Add("queue", new Link { Id = "vocab:Customer/queue" });
            Add("spaces", new Link { Id = "vocab:Customer/spaces" });
        }
    }

    public class CustomerClass : Class
    {
        public CustomerClass()
        {
            Id = "vocab:Customer";
            Label = "Customer";
            Description = "The route to all of your assets in the DLCS";
            SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_retrieve",
                    Method = "GET",
                    Label = "Obtain a customer",
                    Returns = "vocab:Customer"
                }
            };
            SupportedProperties = new[]
            {
                new SupportedProperty
                {
                    Title = "name",
                    Description = "The URL-friendly name of the customer",
                    ReadOnly = false,
                    WriteOnly = false,
                    Property = new RdfProperty
                    {
                        Id = "vocab:Customer/name",
                        Label = "name",
                        Description = "The url-friendly name",
                        Domain = "vocab:Customer",
                        Range = Names.XmlSchema.String
                    }
                },
                new SupportedProperty
                {
                    Title = "displayName",
                    Description = "The display name of the customer",
                    ReadOnly = false,
                    WriteOnly = false,
                    Property = new RdfProperty
                    {
                        Id = "vocab:Customer/displayName",
                        Label = "displayName",
                        Description = "The Customer's display name",
                        Domain = "vocab:Customer",
                        Range = Names.XmlSchema.String
                    }
                },
                new SupportedProperty
                {
                    Title = "portalUsers",
                    Description = "Accounts that can log into the portal",
                    ReadOnly = true,
                    WriteOnly = false,
                    Property = new HydraLinkProperty
                    {
                        Id = "vocab:Customer/portalUsers",
                        Label = "Portal Users",
                        Description = "The collection of a customer's users who can log into the portal",
                        Domain = "vocab:Customer",
                        Range = Names.Hydra.Collection,
                        SupportedOperations = new []
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
                                StatusCodes = new []
                                {
                                    new Status
                                    {
                                        StatusCode = 201,
                                        Description = "User is ready..."
                                    } 
                                }
                            }
                        }
                    }
                },

                // Fill in the missing properties...

                new SupportedProperty
                {
                    Title = "queue",
                    Description = "The resource to which image ingests are submitted",
                    ReadOnly = true,
                    WriteOnly = false,
                    Property = new HydraLinkProperty
                    {
                        Id = "vocab:Customer/queue",
                        Label = "Queue",
                        Description = "The Customer's view on the DLCS ingest queue",
                        Domain = "vocab:Customer",
                        Range = Names.Hydra.Collection, // of batches
                        SupportedOperations = new []
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
                                StatusCodes = new []
                                {
                                    new Status
                                    {
                                        StatusCode = 202,
                                        Description = "Job has been accepted"
                                    }
                                }
                            }
                        }
                    }
                },
                new SupportedProperty
                {
                    Title = "spaces",
                    Description = "A space allows you to partition images, have different default rules, etc",
                    ReadOnly = true,
                    WriteOnly = false,
                    Property = new HydraLinkProperty
                    {
                        Id = "vocab:Customer/spaces",
                        Label = "Spaces",
                        Description = "The collection of a customer's spaces",
                        Domain = "vocab:Customer",
                        Range = Names.Hydra.Collection,
                        SupportedOperations = new []
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
                                StatusCodes = new []
                                {
                                    new Status
                                    {
                                        StatusCode = 201,
                                        Description = "Space is ready..."
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
