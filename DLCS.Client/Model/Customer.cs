﻿using Hydra;
using Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(CustomerClass), 
        Description = "A customer represents you, the API user. You only have access to one customer, " +
                      "so it is your effective entry point for the API. The only interation you can have with " +
                      "your Customer resource directly is updating the display name, but it provides links (🔗) to" +
                      "collections of all the other resources.",
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
        [HydraLink(Description = "Collection of user accounts that can log into the portal. Use this to grant access to others in your organisation",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "portalUsers")]
        public string PortalUsers { get; set; }
        
        [HydraLink(Description = "Collection of all the Named Queries you have configured (plus those provided 'out of the box'). " +
                                 "See the NamedQuery topic for further information",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "namedQueries")]
        public string NamedQueries { get; set; }

        [HydraLink(Description = "Collection of configuration settings for retrieving your registered images from their origin URLs. If your" +
                                 " images come from multiple locations you will have multiple origin strategies. See the OriginStrategy topic.",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 14, PropertyName = "originStrategies")]
        public string OriginStrategies { get; set; }

        [HydraLink(Description = "Collection of IIIF Authentication services available for use with your images. The images are" +
                                 " associated with the auth services via Roles. An AuthService is a means of acquirung a role.",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 15, PropertyName = "authServices")]
        public string AuthServices { get; set; }

        [HydraLink(Description = "Collection of the available roles you can assign to your images. In order for a user to see an image, the " +
                                 "user must have the role associated with the image, or one of them. Users interact with an AuthService to " +
                                 "acquire a role or roles.",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 16, PropertyName = "roles")]
        public string Roles { get; set; }

        [HydraLink(Description = "The Customer's view on the DLCS ingest queue. As well as allowing you to query the status of batches you " +
                                 "have registered, you can POST new batches to the queue.",
            Range = "vocab:Queue", ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 17, PropertyName = "queue")]
        public string Queue { get; set; }

        [HydraLink(Description = "Collection of all the Space resources associated with your customer. A space allows you to " +
                                 "partition images, have different default roles and tags, etc. See the Space topic.",
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
            string operationId = "_:customer_";

            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                operationId, "Customer", Id,
                "GET");

            // Hydra link properties - i.e., a link to another resource, rather than a field of the current resource.

            GetHydraLinkProperty("portalUsers").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "portalUser_", "Portal User", "vocab:PortalUser");

            GetHydraLinkProperty("namedQueries").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "namedQuery_", "Named Query", "vocab:NamedQuery");

            GetHydraLinkProperty("originStrategies").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "originStrategy_", "Origin Strategy", "vocab:OriginStrategy");

            GetHydraLinkProperty("authServices").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "authService_", "Auth Service", "vocab:AuthService");

            GetHydraLinkProperty("roles").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "role_", "Role", "vocab:Role");

            GetHydraLinkProperty("queue").SupportedOperations = QueueClass.GetSpecialQueueOperations();

            GetHydraLinkProperty("roles").SupportedOperations = CommonOperations
                .GetStandardCollectionOperations(operationId + "space_", "Space", "vocab:Space");

        }
    }
}
