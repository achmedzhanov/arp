using System;
using System.Collections.Generic;

namespace Arp.Generator.Preprocessing.Impl
{
    public class AttributesGroupGenerationInfo : IInfoRef<ICollection<AttributeGenerationInfo>>
    {
        private readonly string name;
        private readonly List<AttributeGenerationInfo> attributes = new List<AttributeGenerationInfo>();


        public AttributesGroupGenerationInfo(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            this.name = name;
        }


        public string Name
        {
            get { return name; }
        }


        public List<AttributeGenerationInfo> Attributes
        {
            get { return attributes; }
        }

        #region IInfoRef<ICollection<AttributeGenerationInfo>> Members

        public ICollection<AttributeGenerationInfo> Get()
        {
            return Attributes;
        }

        #endregion
    }
}