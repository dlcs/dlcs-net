using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
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

        public Queue(int customerId, int size)
        {
            ModelId = customerId;
            Size = size;
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
            SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_retrieve",
                    Method = "GET",
                    Label = "Get the Queue object for a given customer",
                    Returns = Id
                }
            };

            // you can't POST a batch - you do this by posting Image[] to Queue.
            // Or do you?
            var batches = GetHydraLinkProperty("batches");
            batches.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_batches_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all batches for customer",
                    Returns = Names.Hydra.Collection
                }
            };
        }
    }
}
