using System;

namespace AutoMapperExt.AutoMapper
{
    public class AutoMapAttribute : Attribute
    {
        public Type[] ToSource { get; private set; }

        public AutoMapAttribute(params Type[] toSource)
        {
            this.ToSource = toSource;
        }
    }
}
