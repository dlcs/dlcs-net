using Hydra;
using Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof (QueueClass),
        Description = "Your current ingesting images",
        UriTemplate = "/customers/{0}/queue")]
    public class Queue : DlcsResource
    {
        [JsonIgnore]
        public int ModelId { get; set; }

        public Queue()
        {
        }

        public Queue(int customerId)
        {
            ModelId = customerId;
            Init(true, customerId);
        }

        [RdfProperty(Description = "Number of total images in your queue, across batches",
            Range = Names.XmlSchema.NonNegativeInteger, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "size")]
        public int Size { get; set; }

        [HydraLink(Description = "Separate jobs you have submitted",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "batches")]
        public string Batches { get; set; }

        [HydraLink(Description = "Merged view of images on the queue, across batches",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "images")]
        public string Images { get; set; }

    }


    public class QueueClass : Class
    {
        public QueueClass()
        {
            BootstrapViaReflection(typeof (Queue));
        }

        public override void DefineOperations()
        {
            SupportedOperations = GetSpecialQueueOperations();

            // you can't POST a batch - you do this by posting Image[] to Queue.
            // Or do you?
            GetHydraLinkProperty("batches").SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_batch_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all batches for customer",
                    Returns = Names.Hydra.Collection
                }
            };
        }

        public static Operation[] GetSpecialQueueOperations()
        {
            return new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_retrieve",
                    Method = "GET",
                    Label = "Returns the queue resource",
                    Returns = "vocab:Queue"
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
        } 
    }
}
