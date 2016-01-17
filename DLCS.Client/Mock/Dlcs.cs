using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLCS.Client.Images;
using DLCS.Client.PDFs;

namespace DLCS.Client.Mock
{
    public class Dlcs : IDlcs
    {
        private readonly string imageRegistrationUri;
        public Dlcs(string imageRegistrationUri)
        {
            this.imageRegistrationUri = imageRegistrationUri;
        }

        public Operation<ImageQuery, Image[]> GetImages(ImageQuery query)
        {
            throw new NotImplementedException();
        }

        public Operation<ImageQuery, ImageStateQueryResponse> GetImageStates(ImageQuery query)
        {
            throw new NotImplementedException();
        }

        public MakePdfResponse MakePdf(MakePdfRequest pdfRequest)
        {
            throw new NotImplementedException();
        }
        

        public Operation<Image[], ImageBatchRegistration> RegisterImages(Image[] images)
        {
            throw new NotImplementedException();
        }
    }
}
