using DLCS.Client.Config;
using Hydra;
using Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(EntryPointClass), 
        Description = "The main entry point or homepage of the API.",
        UriTemplate = "")]
    public class EntryPoint : DlcsResource
    {
        [HydraLink(Description = "List of customers to which you have access (usually 1)", 
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "customers")]
        public string Customers { get; set; }
    }

    public class EntryPointClass : Class
    {
        public EntryPointClass()
        {
            BootstrapViaReflection(typeof(EntryPoint));
        }

        public override void DefineOperations()
        {
            SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:entry_point",
                    Method = "GET",
                    Label = "The API's main entry point.",
                    Returns = Id
                }
            };

            var customers = GetHydraLinkProperty("customers");
            customers.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all Customer entities",
                    Returns = Names.Hydra.Collection
                }
            };
        }
    }
}
