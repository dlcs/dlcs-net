
using System;

namespace DLCS.Client
{
    public class Operation<TRequest, TResponse>
    {
        public Uri Uri { get; internal set; }
        public TRequest RequestObject { get; internal set; }
        public TResponse ResponseObject { get; internal set; }
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }
        public Error Error { get; internal set; }
        public string HttpMethod { get; internal set; }

    }
}
