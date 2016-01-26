using DLCS.Client.Interface;
using DLCS.Client.Model;
using DLCS.Client.Model.Images;
using Image = DLCS.Client.Model.Images.Image;

namespace DLCS.Client.Dlcs
{
    public class Customer : ICustomer
    {
        private Dlcs _dlcs;

        public Customer(Dlcs dlcs)
        {
            _dlcs = dlcs;
        }

        public Operation<ImageQuery, Image[]> GetImages(ImageQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Operation<ImageQuery, Space> GetSpace(int space, ImageQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
