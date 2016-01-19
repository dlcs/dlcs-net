using DLCS.Client.Model.Images;

namespace DLCS.Client.Interface
{
    public interface IQueue
    {
        /// <summary>
        /// Add images to the queue.
        /// POST /c/queue
        /// Image[]
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        Operation<Image[], ImageBatchRegistration> RegisterImages(Image[] images);

        /// <summary>
        /// Query the queue for ingest status
        /// GET /c/queue?q={imageQuery}
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Operation<ImageQuery, ImageStateQueryResponse> GetImageStates(ImageQuery query);
    }
}
