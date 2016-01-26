using System;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(PortalUserClass),
        Description = "A user of the portal",
        UriTemplate = "/customers/{0}/portalUsers/{1}")]
    public class PortalUser : DlcsResource
    {
        [JsonIgnore]
        public string ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }

        public PortalUser() { }

        public PortalUser(int customerId, string userId, string email, DateTime created, string[] roles, bool enabled)
        {
            CustomerId = customerId;
            ModelId = userId;
            Email = email;
            Created = created;
            Role = roles;
            Enabled = enabled;
            Init(true, customerId, ModelId);
        }


        [RdfProperty(Description = "The email address",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "email")]
        public string Email { get; set; }

        [RdfProperty(Description = "Create date",
            Range = Names.XmlSchema.DateTime, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "created")]
        public DateTime Created { get; set; }

        [RdfProperty(Description = "List of Role URIs that the user has",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "role")]
        public string[] Role { get; set; }

        [RdfProperty(Description = "Whether the user can log in",
            Range = Names.XmlSchema.Boolean, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 14, PropertyName = "enabled")]
        public bool Enabled { get; set; }
    }

    public class PortalUserClass : Class
    {
        public PortalUserClass()
        {
            BootstrapViaReflection(typeof (PortalUser));
        }

        public override void DefineOperations()
        {
            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                "_:customer_portalUser_", "Portal User", Id, 
                "GET", "PUT", "PATCH", "DELETE");
        }
    }
}
