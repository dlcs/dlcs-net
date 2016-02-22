﻿using System.Linq;
using DLCS.HydraModel.Config;
using Hydra;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DLCS.HydraModel.Model
{
    public class DlcsResource : JSONLDBase
    {
        public void Init(bool setLinks, params object[] urlParams)
        {
            var hydraClassAttr = GetType().GetCustomAttributes(true).OfType<HydraClassAttribute>().Single();
            Id = BaseUrl + string.Format(hydraClassAttr.UriTemplate, urlParams);
            if (setLinks)
            {
                SetHydraLinkProperties();
            }
        }

        /// <summary>
        /// There is a better way of doing this, it's really slow - I'm trying to avoid having to hard code anything...
        /// We don't always want these set, e.g., if we're returning a collection and we want to keep the JSON payload low.
        /// 
        /// </summary>
        private void SetHydraLinkProperties()
        {
            foreach (var property in GetType().GetProperties())
            {
                var attrs = property.GetCustomAttributes(true);
                var jsonProp = attrs.OfType<JsonPropertyAttribute>().SingleOrDefault();
                if (jsonProp != null)
                {
                    var hydraLink = attrs.OfType<HydraLinkAttribute>().SingleOrDefault();
                    if (hydraLink != null && hydraLink.SetManually == false)
                    {
                        property.SetValue(this, Id + "/" + jsonProp.PropertyName);
                    }
                }
            }
        }

        public virtual JObject GetCollectionForm()
        {
            dynamic jo = new JObject();
            jo["@id"] = Id;
            jo["@type"] = Type;
            return jo;
        }

        public override string Context
        {
            // do this better later - prefer not have FullName but makes reflection easier!
            get { return BaseUrl + "/contexts/" + GetType().FullName + ".jsonld"; } 
        }

        public override string Type
        {
            get { return GetType().Name; }
        }
        
        [JsonIgnore]
        protected string BaseUrl
        {
            get { return Constants.BaseUrl; }
        }

    }
}