using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DLCS.Mock.ApiApp;

namespace DLCS.Mock.Controllers
{
    public class MockController : ApiController
    {
        private static MockModel _model;
        private static readonly object ModelLock = new object();




        public MockModel GetModel()
        {
            if (_model == null)
            {
                lock (ModelLock)
                {
                    if (_model == null)
                    {
                        _model = MockModel.Build();
                    }
                }
            }
            return _model;
        }

        public MockModel RebuildModel()
        {
            lock (ModelLock)
            {
                _model = MockModel.Build();
            }
            return _model;
        }

        public MockModel RecalculateCounters()
        {
            lock (ModelLock)
            {
                MockModel.RecalculateCounters(_model);
            }
            return _model;
        }
    }
}
