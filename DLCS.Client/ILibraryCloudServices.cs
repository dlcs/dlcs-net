using DLCS.Client.Images;
using DLCS.Client.PDFs;

namespace DLCS.Client
{
    public interface ILibraryCloudServices
    {
        Operation<Image[], ImageBatchRegistration> RegisterImages(Image[] images);

        Operation<ImageQuery, ImageStateQueryResponse> GetImageStates(ImageQuery query);

        Operation<ImageQuery, Image[]> GetImages(ImageQuery query);

        MakePdfResponse MakePdf(MakePdfRequest pdfRequest);
    }
}
