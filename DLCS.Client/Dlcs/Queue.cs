using System;
using DLCS.Client.Interface;
using DLCS.Client.Model.Images;

namespace DLCS.Client.Dlcs
{
    public class Queue : IQueue
    {
        private readonly Dlcs _dlcs;

        public Queue(Dlcs dlcs)
        {
            _dlcs = dlcs;
        }

        private static Uri _imageQueueUri;
        private void InitQueue()
        {
            if (_imageQueueUri == null)
            {
                // TODO: At this point we would work out RESTfully where the queue for this customer is
                // and cache that for a bit - we assume the API stays reasonably stable.
                _imageQueueUri = new Uri(_dlcs.DlcsConfig.ApiEntryPoint + _dlcs.DlcsConfig.CustomerId + "/queue");
            }
        }

        public Operation<Image[], ImageBatchRegistration> RegisterImages(Image[] images)
        {
            InitQueue();
            return _dlcs.PostOperation<Image[], ImageBatchRegistration>(images, _imageQueueUri);
        }


        public Operation<ImageQuery, ImageStateQueryResponse> GetImageStates(ImageQuery query)
        {
            InitQueue();
            return _dlcs.GetOperation<ImageQuery, ImageStateQueryResponse>(query, _imageQueueUri);
        }
    }
}
