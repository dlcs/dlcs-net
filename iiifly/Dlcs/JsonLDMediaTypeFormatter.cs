using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iiifly.Dlcs
{
    public class JsonLDMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private static readonly MediaTypeHeaderValue JsonLdMediaType = new MediaTypeHeaderValue("application/ld+json");
        public JsonLDMediaTypeFormatter() : base()
        {
            this.SupportedMediaTypes.Add(JsonLdMediaType);
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}
