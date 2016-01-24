using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(EntryPointClass))]
    [HydraContext(typeof(EntryPointContext))]
    public class EntryPoint : DlcsResource
    {
        public override string Type
        {
            get { return "EntryPoint"; }
        }

        [JsonProperty(Order = 11, PropertyName = "customers")]
        public string Customers { get; set; }
    }

    public class EntryPointContext : DlcsClassContext
    {
        public EntryPointContext()
        {
            Add("EntryPoint", "vocab:EntryPoint");
            Add("customers", new Link { Id = "vocab:EntryPoint/customers" });
        }
    }

    public class EntryPointClass : Class
    {
        public EntryPointClass()
        {
            Id = "vocab:EntryPoint";
            Label = "EntryPoint";
            Description = "The main entry point or homepage of the API.";
            SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:entry_point",
                    Method = "GET",
                    Label = "The API's main entry point.",
                    Returns = "vocab:EntryPoint"
                }
            };
            SupportedProperties = new[]
            {
                new SupportedProperty
                {
                    Title = "customers",
                    Description = "List of customers to which you have access (usually 1)",
                    ReadOnly = true,
                    WriteOnly = false,
                    Property = new HydraLinkProperty
                    {
                        Id = "vocab:EntryPoint/customers",
                        Label = "customers",
                        Description = "The collection of all customers",
                        Domain = "vocab:EntryPoint",
                        Range = Names.Hydra.Collection,
                        SupportedOperations = new []
                        {
                            new Operation
                            {
                                Id = "_:customer_collection_retrieve",
                                Method = "GET",
                                Label = "Retrieves all Customer entities",
                                Returns = Names.Hydra.Collection
                            }
                        }
                    }
                }
            };
        }
    }

}
