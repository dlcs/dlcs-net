using Hydra;
using Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(OriginStrategyClass),
        Description = "As a customer you can provide information to the DLCS to allow it to fetch your images " +
                      "from their origin endpoints. Every customer has a default origin strategy, which is for the " +
                      "DLCS to attempt to fetch the image from its origin URL without presenting credentials. " +
                      "This is fine for images that are publicly available, but is unlikely to be appropriate for " +
                      "images you are exposing from your asset management system.You might have a service that is " +
                      "available only to the DLCS, or an FTP site.",
        UriTemplate = "/customers/{0}/originStrategies/{1}")]
    public class OriginStrategy : DlcsResource
    {
        [JsonIgnore]
        public string ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }

        public OriginStrategy() { }

        public OriginStrategy(int customerId, string strategyId, string regex, string protocol, string credentials)
        {
            CustomerId = customerId;
            ModelId = strategyId;
            Regex = regex;
            Protocol = protocol;
            Credentials = credentials;
            Init(true, customerId, ModelId);
        }

        [RdfProperty(Description = "Regex for matching origin",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "regex")]
        public string Regex { get; set; }

        [RdfProperty(Description = "The protocol to use, if it can't be deduced from the regex",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "protocol")]
        public string Protocol { get; set; }

        [RdfProperty(Description = "JSON object - credentials appropriate to the protocol, will vary",
            Range = Names.XmlSchema.String, ReadOnly = false, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "credentials")]
        public string Credentials { get; set; }
    }

    public class OriginStrategyClass : Class
    {
        public OriginStrategyClass()
        {
            BootstrapViaReflection(typeof(OriginStrategy));
        }

        public override void DefineOperations()
        {
            SupportedOperations = CommonOperations.GetStandardResourceOperations(
                "_:customer_originStrategy_", "Origin Strategy", Id,
                "GET", "PUT", "PATCH", "DELETE");
        }
    }
}
