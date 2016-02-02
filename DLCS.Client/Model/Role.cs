﻿using Hydra;
using Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(RoleClass),
        Description = "A role is used by the DLCS to enforce access control. Images have roles." +
                      "The DLCS acquires a user's roles from a RoleProvider. In the case of the simple " +
                      "Clickthrough role, the DLCS can supply this role to the user, but in other scenarios " +
                      "the DLCS needs to acquire roles for the user from the customer's endpoints.",
        UriTemplate = "/customers/{0}/roles/{1}")]
    public class Role : DlcsResource
    {
        [JsonIgnore]
        public string ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }

        public Role() { }

        public Role(int customerId, string roleId, string name, string label, string[] aliases)
        {
            CustomerId = customerId;
            ModelId = roleId;
            Name = name;
            Label = label;
            Aliases = aliases;
            Init(true, customerId, ModelId);
        }


        [RdfProperty(Description = "The role name - this might be the same as the ID?",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "name")]
        public string Name { get; set; }

        [RdfProperty(Description = "Label for a slightly longer description of the role",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "label")]
        public string Label { get; set; }

        [RdfProperty(Description = "If the DLCS acquires roles from the customer, they might have different names",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "aliases")]
        public string[] Aliases { get; set; }

        [HydraLink(Description = "The IIIF Auth Service for this role",
            Range = "vocab:AuthService", ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 15, PropertyName = "authService")]
        public string AuthService { get; set; }
    }

    public class RoleClass : Class
    {
        public RoleClass()
        {
            BootstrapViaReflection(typeof(Role));
        }

        public override void DefineOperations()
        {
            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                "_:customer_role_", "Role", Id,
                "GET", "PUT", "PATCH", "DELETE");
            
            GetHydraLinkProperty("authService").SupportedOperations = CommonOperations.GetStandardResourceOperations(
                "_:customer_authService_", "Auth Service", Id,
                "GET", "PUT", "PATCH", "DELETE");
        }
    }
}
