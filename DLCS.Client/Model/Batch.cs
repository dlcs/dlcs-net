using System;
using DLCS.Client.Hydra;
using DLCS.Client.Hydra.Model;
using Newtonsoft.Json;

namespace DLCS.Client.Model
{
    [HydraClass(typeof(BatchClass),
           Description = "Represents a submitted job of images",
           UriTemplate = "/customers/{0}/queue/batches/{1}")]
    public class Batch : DlcsResource
    {
        [JsonIgnore]
        public string ModelId { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }

        public Batch() { }

        public Batch(string modelId, int customerId, DateTime submitted, int count, DateTime completed, int errors,
            DateTime estCompletion)
        {

            ModelId = modelId;
            CustomerId = customerId;
            Submitted = submitted;
            Count = count;
            Completed = completed;
            Errors = errors;
            EstCompletion = estCompletion;
            Init(true, customerId, modelId);
        }


        [RdfProperty(Description = "Date the batch was POSTed",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 11, PropertyName = "submitted")]
        public DateTime Submitted { get; set; }

        [RdfProperty(Description = "Total number of images in the batch",
            Range = Names.XmlSchema.NonNegativeInteger, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 12, PropertyName = "count")]
        public int Count { get; set; }

        [RdfProperty(Description = "Date the batch was completed (may still have errors)",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 13, PropertyName = "completed")]
        public DateTime Completed { get; set; }

        [RdfProperty(Description = "Total number of error images in the batch",
            Range = Names.XmlSchema.NonNegativeInteger, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 14, PropertyName = "errors")]
        public int Errors { get; set; }

        [RdfProperty(Description = "Estimated Completion (best guess as to when this batch might get done)",
            Range = Names.XmlSchema.DateTime, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 15, PropertyName = "estCompletion")]
        public DateTime EstCompletion { get; set; }

        [HydraLink(Description = "All the images in the batch",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "images")]
        public string Images { get; set; }

        [HydraLink(Description = "Images that have completed processing",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "completedImages")]
        public string CompletedImages { get; set; }

        [HydraLink(Description = "Images that encountered errors",
            Range = Names.Hydra.Collection, ReadOnly = true, WriteOnly = false)]
        [JsonProperty(Order = 20, PropertyName = "errorImages")]
        public string ErrorImages { get; set; }
    }

    public class BatchClass : Class
    {
        public BatchClass()
        {
            BootstrapViaReflection(typeof(Batch));
        }

        public override void DefineOperations()
        {
            SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_batch_retrieve",
                    Method = "GET",
                    Label = "Obtain a batch",
                    Returns = Id
                }
            };

            var images = GetHydraLinkProperty("images");
            images.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_batch_images_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all images in batch",
                    Returns = Names.Hydra.Collection
                }
            };

            var completedImages = GetHydraLinkProperty("completedImages");
            completedImages.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_batch_completedImages_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all COMPLETED images in batch",
                    Returns = Names.Hydra.Collection
                }
            };

            var errorImages = GetHydraLinkProperty("errorImages");
            errorImages.SupportedOperations = new[]
            {
                new Operation
                {
                    Id = "_:customer_queue_batch_errorImages_collection_retrieve",
                    Method = "GET",
                    Label = "Retrieves all ERROR images in batch",
                    Returns = Names.Hydra.Collection
                }
            };

        }
    }
}
