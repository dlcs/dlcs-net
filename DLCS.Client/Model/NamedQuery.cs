using Hydra;
using Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(NamedQueryClass),
        Description = "A stored query that will generate IIIF manifests",
        UriTemplate = "/customers/{0}/namedQueries/{1}")]
    public class NamedQuery : DlcsResource
    {
        [JsonIgnore]
        public string ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }

        public NamedQuery() { }

        public NamedQuery(int customerId, string queryName, string data)
        {
            CustomerId = customerId;
            ModelId = queryName;
            Data = data;
            Init(true, customerId, ModelId);
        }

        [RdfProperty(Description = "The data for the query. JSON object?",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "data")]
        public string Data { get; set; }

    }

    public class NamedQueryClass : Class
    {
        public NamedQueryClass()
        {
            BootstrapViaReflection(typeof(NamedQuery));
        }

        public override void DefineOperations()
        {
            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                "_:customer_namedqueries_", "Named Query", Id,
                "GET", "PUT", "PATCH", "DELETE");
        }
    }
}
