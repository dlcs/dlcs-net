using DLCS.Client.Interface;
using DLCS.Client.Model;
using DLCS.Client.Model.Images;
using DLCS.Client.PDFs;

namespace DLCS.Client
{
    public interface IDlcs
    {
        IQueue Queue { get; }
        ICustomer Customer { get; }


        MakePdfResponse MakePdf(MakePdfRequest pdfRequest);
    }
}
