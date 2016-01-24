using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DLCS.Client.Hydra.Model
{
    public abstract class Class : JSONLDBaseWithHydraContext
    {
        public abstract void DefineOperations();

        public override string Type
        {
            get { return "Class"; }
        }
        

        [JsonProperty(Order = 11, PropertyName = "subClassOf")]
        public string SubClassOf { get; set; }

        [JsonProperty(Order = 12, PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(Order = 13, PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(Order = 20, PropertyName = "supportedOperation")]
        public Operation[] SupportedOperations { get; set; }

        [JsonProperty(Order = 21, PropertyName = "supportedProperty")]
        public SupportedProperty[] SupportedProperties{ get; set; }

        public HydraLinkProperty GetHydraLinkProperty(string title)
        {
            return SupportedProperties
                .Where(p => p.Property is HydraLinkProperty)
                .Single(p => p.Title == title)
                .Property as HydraLinkProperty;
        }

        /// <summary>
        /// I'm trying to strike a balance between excessive reflection and ease of coding
        /// </summary>
        /// <param name="resourceType"></param>
        protected void BootstrapViaReflection(Type resourceType)
        {
            var classAttr = (HydraClassAttribute)resourceType
                .GetCustomAttributes(typeof(HydraClassAttribute), true).First();
            Id = "vocab:" + resourceType.Name;
            Label = resourceType.Name;
            Description = classAttr.Description;

            // We won't get Supported Operations by reflecting on attributes - hard to read
            // Requring it there does not involve any duplication, which is the main reason
            // to do this via attributes.

            // We will stub out SupportedProperties to avoid duplication, but again use the
            // class definition to flesh it out fully.
            List<SupportedProperty> supportedProperties = new List<SupportedProperty>();
            foreach (var property in resourceType.GetProperties())
            {
                var attrs = property.GetCustomAttributes(true);
                var jsonProp = attrs.OfType<JsonPropertyAttribute>().SingleOrDefault();
                if (jsonProp != null)
                {
                    var rdfProp = attrs.OfType<RdfPropertyAttribute>().SingleOrDefault();
                    var hydraLink = attrs.OfType<HydraLinkAttribute>().SingleOrDefault();
                    var propAttribute = rdfProp ?? (SupportedPropertyAttribute) hydraLink;
                    if (propAttribute != null)
                    {
                        var prop = new SupportedProperty
                        {
                            Title = jsonProp.PropertyName,
                            Description = propAttribute.Description,
                            ReadOnly = propAttribute.ReadOnly,
                            WriteOnly = propAttribute.WriteOnly
                        };
                        if (rdfProp != null)
                        {
                            prop.Property = new RdfProperty();
                        }
                        if (hydraLink != null)
                        {
                            prop.Property = new HydraLinkProperty();
                        }
                        prop.Property.Id = Id + "/" + jsonProp.PropertyName;
                        prop.Property.Label = jsonProp.PropertyName;
                        prop.Property.Description = propAttribute.Description;
                        prop.Property.Domain = Id;
                        prop.Property.Range = propAttribute.Range;
                        supportedProperties.Add(prop);
                    }
                }
            }
            if (supportedProperties.Count > 0)
            {
                SupportedProperties = supportedProperties.ToArray();
            }
            DefineOperations();
        } 
    }
}
