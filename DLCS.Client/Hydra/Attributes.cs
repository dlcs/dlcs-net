using System;

namespace DLCS.Client.Hydra
{
    /// <summary>
    /// Provide a means for an attribute to point at another type
    /// </summary>
    public class TypeReferencingAttribute : Attribute
    {
        public Type ReferencedType;

        public TypeReferencingAttribute(Type t)
        {
            ReferencedType = t;
        }
    }

    public class HydraClassAttribute : TypeReferencingAttribute
    {
        public HydraClassAttribute(Type t) : base(t) {}
        public string Description { get; set; }
        public string UriTemplate { get; set; }
    }

    public class SupportedPropertyAttribute : Attribute
    {
        public string Description { get; set; }
        public bool ReadOnly { get; set; }
        public bool WriteOnly { get; set; }
        public string Range { get; set; }
    }

    public class RdfPropertyAttribute : SupportedPropertyAttribute { }

    public class HydraLinkAttribute : SupportedPropertyAttribute { }


    //public class HydraContextAttribute : TypeReferencingAttribute
    //{
    //    public HydraContextAttribute(Type t) : base(t) {}
    //}
}
