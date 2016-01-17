﻿
namespace DLCS.Client.Images
{
    public class Operation<TRequest, TResponse>
    {
        public TRequest RequestObject { get; internal set; }
        public TResponse ResponseObject { get; internal set; }
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }
        public Error Error { get; internal set; }

    }
}
