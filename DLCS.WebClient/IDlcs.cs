using DLCS.WebClient.Dlcs;
using DLCS.WebClient.Model;
using DLCS.WebClient.Model.Images;

namespace DLCS.WebClient
{
    public interface IDlcs
    {
        /// <summary>
        /// Add images to the queue.
        /// POST /c/queue
        /// Image[]
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        Operation<HydraImageCollection, Batch> RegisterImages(HydraImageCollection images);

        /// <summary>
        /// Query the queue for ingest status
        /// GET /c/queue?q={imageQuery}
        /// </summary>
        /// <param name="query"></param>
        /// <param name="defaultSpace"></param>
        /// <returns></returns>
        Operation<ImageQuery, HydraImageCollection> GetImages(ImageQuery query, int defaultSpace);


        Operation<ImageQuery, HydraImageCollection> GetImages(string nextUri);

        string GetRoleUri(string accessCondition);
    }
}
