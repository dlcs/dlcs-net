using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLCS.Client.Model;
using DLCS.Client.Model.Images;

namespace DLCS.Client.Interface
{
    public interface ICustomer
    {
        /// <summary>
        /// If space supplied, query space.
        /// If not, query allimages.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Operation<ImageQuery, Image[]> GetImages(ImageQuery query);

        Operation<ImageQuery, Space> GetSpace(int space, ImageQuery query);

    }
}
