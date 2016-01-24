using System;

namespace DLCS.Client.Hydra
{
    public class HydraAttribute : Attribute
    {
        public Type ReferencedType;

        public HydraAttribute(Type t)
        {
            ReferencedType = t;
        }
        
    }

    public class HydraClassAttribute : HydraAttribute
    {
        public HydraClassAttribute(Type t) : base(t)
        {
        }
    }

    public class HydraContextAttribute : HydraAttribute
    {
        public HydraContextAttribute(Type t) : base(t)
        {
        }
    }
}
